using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeTrigger : MonoBehaviour
{

    [SerializeField] AudioClip death;
    [SerializeField] AudioSource trigger;
    [SerializeField] float volume;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trigger.PlayOneShot(death, volume);
            gameManager.instance.playerScript.death();

        }
    }
}
