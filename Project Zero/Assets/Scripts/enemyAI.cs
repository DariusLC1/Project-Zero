using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour, IDamageable
{
    [Header("-----Components-----")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Renderer rend;
    [SerializeField] Animator anim;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip hurting;
    [SerializeField] float hurtingVol;
    [SerializeField] AudioClip dying;
    [SerializeField] float dyingVol;

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
    [SerializeField] GameObject Health;
    [SerializeField] GameObject Ammo;

    Vector3 playerDirection;
    Vector3 startingPos;
    bool isShooting = false;
    bool detection = false;
    float stoppingDistanceOrig;
    float speedOrig;


    // Start is called before the first frame update
    void Start()
    {
        speedOrig = agent.speed;
        stoppingDistanceOrig = agent.stoppingDistance;
        startingPos = transform.position;
        roam();
    }

    // Update is called once per frame
    void Update()
    {

        if (agent.isActiveAndEnabled && !anim.GetBool("Dead"))
        {
            anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), agent.velocity.normalized.magnitude, Time.deltaTime * 5));

            playerDirection = gameManager.instance.player.transform.position - new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);

            turnToPlayer();

            if (detection)
            {
                canSeePlayer();
            }
            else if (agent.remainingDistance < 0.1f)
            {
                roam();
            }
        }

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
        float angle = Vector3.Angle(playerDirection, transform.forward); //finds angle between the player and the front of the enemy
        RaycastHit hit;

        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), playerDirection, out hit))
        {

            if (hit.collider.CompareTag("Player") && angle <= FOV)
            {
                turnToPlayer();

                agent.SetDestination(gameManager.instance.player.transform.position);
                agent.stoppingDistance = stoppingDistanceOrig;
                agent.speed = speedChase;

                if (!isShooting && angle <= FOVShoot)
                {
                    StartCoroutine(shoot());
                }
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
        anim.SetTrigger("Damage");
        audioSource.PlayOneShot(hurting, hurtingVol);

        if (HP <= 0)
        {
            if (this.CompareTag("Spawner Enemy"))
            {
                gameManager.instance.checkDoorEnemyTotal();
                anim.SetBool("Dead", true);
                agent.enabled = false;
                foreach (Collider col in GetComponents<Collider>())
                    col.enabled = false;
            }

            else 
            { 
            gameManager.instance.checkEnemyTotal();
            anim.SetBool("Dead", true);
            agent.enabled = false;
            foreach (Collider col in GetComponents<Collider>())
                col.enabled = false;
                gameManager.instance.killed++;
                gameManager.instance.killedEnemies.text = $"Killed Enemies | {gameManager.instance.killed}";
                gameManager.instance.playerScript.sheildregen++;
                Instantiate(Ammo);
                Instantiate(Health);
            }

        }
        else
        {
            StartCoroutine(flashColor());
        }

    }

    IEnumerator flashColor()
    {
        rend.material.color = Color.red;
        agent.speed = 0;
        yield return new WaitForSeconds(0.3f);
        agent.speed = speedOrig;
        rend.material.color = Color.white;
        agent.stoppingDistance = 0;
        agent.SetDestination(gameManager.instance.player.transform.position);
    }

    IEnumerator shoot()
    {
        isShooting = true;

        anim.SetTrigger("Shoot");

        GameObject bulletClone = Instantiate(bullet, bulletSpawn.transform.position, bullet.transform.rotation);
        bulletClone.GetComponent<bullet>().damage = damage;
        bulletClone.GetComponent<bullet>().speed = bulletSpeed;
        bulletClone.GetComponent<bullet>().destroyTime = bulletDestroyTime;

        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            detection = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            detection = false;
            agent.stoppingDistance = 0;
        }
    }
}