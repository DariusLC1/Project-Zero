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
                
                Destroy(gameObject);
            }
            else if (gameObject.name == "wall jump pickup")
            {
                gameManager.instance.playerScript.haswalljump = true;
                Destroy(gameObject);
            }

            if (gameManager.instance.playerScript.HP > 50)
            {
                if (gameObject.name == "Health pack")
                {
                    gameManager.instance.playerScript.HP += health;
                    gameManager.instance.playerScript.updatePlayerHP();
                    gameObject.transform.Translate(gameManager.instance.player.transform.position);
                    Destroy(gameObject);
                }
                else if (gameObject.name == "Ammo")
                {
                    gameManager.instance.playerScript.MaxammoCount += ammo;
                    gameManager.instance.playerScript.updateAmmoCount();
                    gameObject.transform.Translate(gameManager.instance.player.transform.position);
                    Destroy(gameObject);
                }
            }
            else if (gameManager.instance.playerScript.HP < 50)
            {
                if (gameObject.name == "Health pack")
                {
                    gameManager.instance.playerScript.HP += health * 2;
                    gameManager.instance.playerScript.updatePlayerHP();
                    gameObject.transform.Translate(gameManager.instance.player.transform.position);
                    Destroy(gameObject);
                }
                else if (gameObject.name == "Ammo")
                {
                    gameManager.instance.playerScript.MaxammoCount += ammo + 5;
                    gameManager.instance.playerScript.updateAmmoCount();
                    gameObject.transform.Translate(gameManager.instance.player.transform.position);
                    Destroy(gameObject);
                }
            }
        }
    }
}