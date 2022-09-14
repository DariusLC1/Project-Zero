using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public static NextLevel Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public async void loadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = true;
        gameManager.instance.playerScript.MaxammoCount = gameManager.instance.Maxammo;

        await System.Threading.Tasks.Task.Delay(1000);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameManager.instance.scenes == 1)
            {
                loadScene("Level 1 boss");
            }
            else if (gameManager.instance.scenes == 2)
            {
                loadScene("Level 2");
            }
            else if (gameManager.instance.scenes == 3)
            {
                loadScene("Level 2 boss");
            }
            else if (gameManager.instance.scenes == 4)
            {
                loadScene("Level 3");
            }
            else if (gameManager.instance.scenes == 5)
            {
                loadScene("Level 3 boss");
            }
            else if (gameManager.instance.scenes == 6)
            {
                loadScene("Final Boss");
            }
        }
    }
}