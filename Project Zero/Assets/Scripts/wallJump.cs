using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallJump : MonoBehaviour
{
    [SerializeField] AudioSource aud;
    [SerializeField] AudioClip cl;
    [SerializeField] float vol;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.instance.playerScript.haswalljump == true)
        {
            aud.PlayOneShot(cl, vol);
            gameManager.instance.playerScript.gravity = gameManager.instance.playerScript.gravity * (float)0.5;
            gameManager.instance.playerScript.jumpsMax = gameManager.instance.playerScript.jumpsMax + 1;
            gameManager.instance.playerScript.jumpsMax = gameManager.instance.playerScript.timesJumped + 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.playerScript.gravity = gameManager.instance.playerScript.gravity * 2;
            gameManager.instance.playerScript.jumpsMax = 2;
        }
    }
}
