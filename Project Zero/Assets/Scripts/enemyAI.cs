using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour, IDamageable
{
    [Header("-----Components-----")]
    [SerializeField] NavMeshAgent agent;

    [Header("-----Stats-----")]
    [Range(0, 10)][SerializeField] int HP;
    [Range(0, 10)][SerializeField] int facePlayer;

    [Header("-----Weapon Stats-----")]
    [Range(0.1f, 5)][SerializeField] float shootRate;
    [Range(1, 10)][SerializeField] int damage;
    [Range(1, 10)][SerializeField] int bulletSpeed;
    [Range(1, 10)][SerializeField] int bulletDestroyTime;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletSpawn;

    Vector3 playerDirection;
    bool isShooting = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(gameManager.instance.player.transform.position);
        playerDirection = gameManager.instance.player.transform.position - transform.position;

        turnToPlayer();
        if (!isShooting)
        {
            StartCoroutine(shoot());
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

}