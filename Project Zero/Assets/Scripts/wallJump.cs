using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallJump : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.playerScript.gravity = gameManager.instance.playerScript.gravity * (float)0.5;
            gameManager.instance.playerScript.timesJumped = 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.playerScript.gravity = gameManager.instance.playerScript.gravity * 2;
        }
    }
}
