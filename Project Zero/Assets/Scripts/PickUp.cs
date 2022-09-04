using UnityEngine;

public class PickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.CompareTag("Health"))
            {
                gameManager.instance.playerScript.HP += 5;
            }
            else if (other.CompareTag("Ammo"))
            {
                gameManager.instance.playerScript.MaxammoCount += gameManager.instance.playerScript.ammoCountOg;
            }
        }
    }
}
