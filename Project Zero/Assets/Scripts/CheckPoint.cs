using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] AudioClip checkpoint;
    [SerializeField] AudioSource trigger;
    [SerializeField] float volume;
    [SerializeField] TextMeshProUGUI text;
    float delay = 4;
    bool firstTime = true;

    private void Awake()
    {
        text.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && firstTime)
        {
            StartCoroutine(ShowCheckPoint());
            trigger.PlayOneShot(checkpoint, volume);
            gameManager.instance.playerSpawnPos.transform.position = transform.position;
            firstTime = false;
        }
    }

    IEnumerator ShowCheckPoint() { 
        text.enabled = true;
        yield return new WaitForSeconds(delay);
        text.enabled = false;
    }
}
