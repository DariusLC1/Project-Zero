using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killarea : MonoBehaviour
{
    int damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            damage = gameManager.instance.playerScript.HP;
            gameManager.instance.playerScript.takeDamage(damage);
        }
    }
}
