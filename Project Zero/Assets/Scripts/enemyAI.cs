using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour, IDamageable
{
    [SerializeField] NavMeshAgent agent;

    [SerializeField] int HP;

    Vector3 playerDirection;
    [SerializeField] int facePlayer;

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

}