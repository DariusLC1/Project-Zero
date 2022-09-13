using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    public int health = 5;
    public int ammo = 10;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(gameObject.name == "Sheild pickup")
            {
                gameManager.instance.playerScript.hassheild = true;
            }
            else if (gameObject.name == "wall jump pickup")
            {
                gameManager.instance.playerScript.haswalljump = true;
            }

            if (gameManager.instance.playerScript.HP > 50)
            {
                Debug.Log("over 50");
                if (gameObject.name == "Health pack")
                {
                    gameObject.transform.Translate(gameManager.instance.player.transform.position);
                    gameManager.instance.playerScript.HP += health;
                    gameManager.instance.playerScript.updatePlayerHP();
                    Destroy(gameObject);
                }
                else if (gameObject.name == "Ammo")
                {
                    gameObject.transform.Translate(gameManager.instance.player.transform.position);
                    gameManager.instance.playerScript.MaxammoCount += ammo;
                    gameManager.instance.playerScript.updateAmmoCount();
                    Destroy(gameObject);
                }
            }
            else if (gameManager.instance.playerScript.HP < 50)
            {
                if (gameObject.name == "Health pack")
                {
                    gameObject.transform.Translate(gameManager.instance.player.transform.position);
                    gameManager.instance.playerScript.HP += health * 2;
                    gameManager.instance.playerScript.updatePlayerHP();
                    Destroy(gameObject);
                }
                else if (gameObject.name == "Ammo")
                {
                    gameObject.transform.Translate(gameManager.instance.player.transform.position);
                    gameManager.instance.playerScript.MaxammoCount += ammo + 5;
                    gameManager.instance.playerScript.updateAmmoCount();
                    Destroy(gameObject);
                }
            }
        }
    }
}