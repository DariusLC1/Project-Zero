using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public static NextLevel Instance;

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
            if (GlobalScript.Instance.scenes == 1 && GlobalScript.Instance.bossRush == false)
            {
                loadScene("Level 1 boss");
            }
            else if (GlobalScript.Instance.scenes == 2 && GlobalScript.Instance.bossRush == false)
            {
                loadScene("Level 2");
            }
            else if (GlobalScript.Instance.scenes == 3 && GlobalScript.Instance.bossRush == false)
            {
                loadScene("Level 3");
            }
            else if (GlobalScript.Instance.scenes == 4 && GlobalScript.Instance.bossRush == false)
            {
                loadScene("Level 3 boss");
            }
            else if (GlobalScript.Instance.scenes == 5 && GlobalScript.Instance.bossRush == false)
            {
                loadScene("Final Boss");
            }

            else if(GlobalScript.Instance.bossRush == true && GlobalScript.Instance.scenes == 1)
            {
                loadScene("Level 3 boss");
            }
            else if (GlobalScript.Instance.bossRush == true && GlobalScript.Instance.scenes == 2)
            {
                loadScene("Final Boss");
            }

        }
    }
}