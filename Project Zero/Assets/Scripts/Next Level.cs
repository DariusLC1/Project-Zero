using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.scenes++;
            if(gameManager.instance.scenes == 1)
            {
                SceneManager.LoadScene("Level 2");
            }
        }
    }
}
