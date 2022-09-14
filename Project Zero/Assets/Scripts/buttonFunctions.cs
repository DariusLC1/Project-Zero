using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class buttonFunctions : MonoBehaviour
{
    // Start is called before the first frame update
    public void resume()
    {
        if (gameManager.instance.isPaused)
        {
            gameManager.instance.isPaused = !gameManager.instance.isPaused;
            gameManager.instance.cursorUnlockUnpause();
        }

    }

    public void startGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void BossRush()
    {
        gameManager.instance.bossRush = true;
        gameManager.instance.playerScript.hassheild = true;
        gameManager.instance.playerScript.haswalljump = true;
        SceneManager.LoadScene("Level 1 boss");
    }

    
    public void respawn()
    {
        gameManager.instance.playerScript.resetHP();
        gameManager.instance.playerScript.respawn();
        gameManager.instance.cursorUnlockUnpause();
        //gameManager.instance.menuCurrentlyOpen.SetActive(false);
    }

    public void restart()
    {
        gameManager.instance.cursorUnlockUnpause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void quit()
    {
        Application.Quit();
    }
}
