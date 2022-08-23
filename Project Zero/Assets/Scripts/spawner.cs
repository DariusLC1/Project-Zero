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

    // Update is called once per frame
    void Update()
    {
        if (spawnAgain && localEnemyCount < enemyMax)
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
}
