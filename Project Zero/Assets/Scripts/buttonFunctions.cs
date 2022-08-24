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
    
    public void respawn()
    {
        gameManager.instance.playerScript.resetHP();
        gameManager.instance.playerScript.respawn();
        gameManager.instance.cursorUnlockUnpause();
        //gameManager.instance.menuCurrentlyOpen.SetActive(false);
    }

    public void restart()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameManager.instance.gameOver = false;
        gameManager.instance.cursorUnlockUnpause();
    }

    public void quit()
    {
        Application.Quit();
    }
}
