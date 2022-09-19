using System;
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
        GlobalScript.Instance.bossRush = true;
        GlobalScript.Instance.hassheild = true;
        GlobalScript.Instance.haswalljump = true;
        WaitUntil();
    }

    private void WaitUntil()
    {
        if (GlobalScript.Instance.haswalljump == true)
        {
            SceneManager.LoadScene("Level 1 boss");
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
        for(int i = 0; i < GlobalScript.Instance.GlobalgunStat.Count; i++)
        {
            GlobalScript.Instance.GlobalgunStat.RemoveAt(i);
        }
    }

    public void quit()
    {
        Application.Quit();
    }
}
