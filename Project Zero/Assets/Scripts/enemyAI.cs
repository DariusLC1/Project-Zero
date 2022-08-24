using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour, IDamageable
{
    [Header("-----Components-----")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Renderer rend;
    //[SerializeField] Animator anim;
    [SerializeField] Transform head;


    [Header("-----Stats-----")]
    [Range(0, 100)][SerializeField] int HP;
    [Range(0, 10)][SerializeField] int facePlayer;
    [Range(15, 360)][SerializeField] int FOV;
    [Range(15, 360)][SerializeField] int FOVShoot;
    [SerializeField] int roamRadius;
    [SerializeField] float speedRoam;
    [SerializeField] float speedChase;

    [Header("-----Weapon Stats-----")]
    [Range(0.1f, 5)][SerializeField] float shootRate;
    [Range(1, 10)][SerializeField] int damage;
    [Range(1, 10)][SerializeField] int bulletSpeed;
    [Range(1, 10)][SerializeField] int bulletDestroyTime;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletSpawn;

    Vector3 playerDirection;
    Vector3 startingPos;
    bool isShooting = false;
    bool playerInRange = false;
    float stoppingDistanceOrig;
    float speedOrig;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (agent.isActiveAndEnabled && !anim.GetBool("Dead"))
        //{
            //anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), agent.velocity.normalized.magnitude, Time.deltaTime * 5));
            playerDirection = gameManager.instance.player.transform.position - head.position;

            if (playerInRange)
            {
                canSeePlayer();
            }
            else if (agent.remainingDistance < 0.1f)
            {
                roam();
            }
        //}
        agent.SetDestination(gameManager.instance.player.transform.position);
        playerDirection = gameManager.instance.player.transform.position - transform.position;

        turnToPlayer();
        if (playerInRange && !isShooting)
        {
            StartCoroutine(shoot());
        }

        if (playerInRange)
        {
            canSeePlayer();
        }
        else if (agent.remainingDistance < 0.1f)
            roam();
    }
    void roam()
    {
        agent.speed = speedRoam;
        agent.stoppingDistance = 0;
        Vector3 RandomDir = Random.insideUnitSphere * roamRadius;
        RandomDir += startingPos;

        NavMeshHit hit;
        NavMesh.SamplePosition(RandomDir, out hit, roamRadius, 1);
        NavMeshPath path = new NavMeshPath();

        agent.CalculatePath(hit.position, path);
        agent.SetPath(path);
    }

    void canSeePlayer()
    {
        float angle = Vector3.Angle(playerDirection, transform.forward);
        Debug.Log(angle);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, playerDirection, out hit))
        {
            Debug.DrawRay(transform.position, playerDirection);

            if (hit.collider.CompareTag("Player") && !isShooting && angle <= FOV)
            {
                agent.SetDestination(gameManager.instance.player.transform.position);
                agent.stoppingDistance = stoppingDistanceOrig;
                agent.speed = speedChase;
                turnToPlayer();

                if (!isShooting && angle <= FOVShoot)
                    StartCoroutine(shoot());
            }

        }

    }

    void turnToPlayer()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            playerDirection.y = 0;
            Quaternion rotate = Quaternion.LookRotation(playerDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotate, Time.deltaTime * facePlayer);
        }
    }

    public void takeDamage(int dmg)
    {
        HP -= dmg;

        if (HP <= 0)
        {
            gameManager.instance.checkEnemyTotal();
            Destroy(gameObject);
        }
    }

    IEnumerator shoot()
    {
        isShooting = true;

        GameObject bulletClone = Instantiate(bullet, bulletSpawn.transform.position, bullet.transform.rotation);
        bulletClone.GetComponent<bullet>().damage = damage;
        bulletClone.GetComponent<bullet>().speed = bulletSpeed;
        bulletClone.GetComponent<bullet>().destroyTime = bulletDestroyTime;

        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}