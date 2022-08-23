using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class spawner : MonoBehaviour
{
    [SerializeField] int enemyMax;

    [SerializeField] int spawnTimer;

    [SerializeField] GameObject enemy;

    bool spawnAgain = true;

    int localEnemyCount;

    bool playerOnTrigger = false;
    // Update is called once per frame

    void Start()
    {
        gameManager.instance.enemyCount += enemyMax;
        
    }
    void Update()
    {
        if (playerOnTrigger && spawnAgain && localEnemyCount < enemyMax)
        {
            StartCoroutine(spawn());
        }
    }

    IEnumerator spawn()
    {
        spawnAgain = false;
        Instantiate(enemy,transform.position,enemy.transform.rotation);
        gameManager.instance.enemyCount++;
        localEnemyCount++;
        yield return new WaitForSeconds(spawnTimer);
        spawnAgain = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnTrigger = false;
        }
    }
}
