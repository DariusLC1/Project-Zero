using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallJumpSoundActivator : MonoBehaviour
{
    [SerializeField] AudioClip wallJump;
    [SerializeField] AudioSource trigger;
    [SerializeField] float volume;
    bool firstTime = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && firstTime)
        {
            trigger.PlayOneShot(wallJump, volume);
            firstTime = false;
        }
    }
}
