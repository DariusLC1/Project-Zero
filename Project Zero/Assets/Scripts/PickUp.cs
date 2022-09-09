using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public int health = 5;
    public int ammo = 10;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameManager.instance.playerScript.HP > 50)
            {
                if (CompareTag("Health"))
                {
                    gameManager.instance.playerScript.HP += health * 2;
                }
                else if (CompareTag("ammo"))
                {
                    gameManager.instance.playerScript.MaxammoCount += ammo + 5;
                }
            }
            else if (gameManager.instance.playerScript.HP < 50)
            {
                if (CompareTag("Health"))
                {
                    gameManager.instance.playerScript.HP += health * 2;
                }
                else if (CompareTag("ammo"))
                {
                    gameManager.instance.playerScript.MaxammoCount += ammo + 5;
                }
            }
        }
    }
}
