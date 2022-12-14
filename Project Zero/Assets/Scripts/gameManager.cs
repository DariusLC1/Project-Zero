using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    public GameObject player;
    public playerController playerScript;

    public GameObject pauseMenu;
    public GameObject menuCurrentlyOpen;
    public GameObject playerDeadMenu;
    public GameObject playerDamageFlash;
    public Image playerHPBar;
    public TextMeshProUGUI ammoCount;
    public TextMeshProUGUI killedEnemies;
    public TextMeshProUGUI ShotsFired;
    public GameObject core;
    public GameObject door;
    public GameObject winMenu;
    public GameObject reloadUI;




    public GameObject playerSpawnPos;
    public int enemyCount;
    public int doorEnemyCount;
    public int killed;
    public int Maxammo;
    public bool bossRush;
    public bool gameOver;
    public bool isPaused = false;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<playerController>();

        playerSpawnPos = GameObject.FindGameObjectWithTag("Player Spawn Pos");
        playerScript.respawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && playerScript.HP > 0)
        {
            isPaused = !isPaused;
            menuCurrentlyOpen = pauseMenu;
            menuCurrentlyOpen.SetActive(isPaused);
            Maxammo = playerScript.MaxammoCount;

            if (isPaused)
            {
                cursorLockPause();
            }
            else
            {
                cursorUnlockUnpause();

            }
        }




    }

    public void cursorLockPause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;

    }

    public void winGame()
    {
        gameOver = true;
        winMenu.SetActive(true);
        menuCurrentlyOpen = winMenu;
        cursorLockPause();
    }

    public void cursorUnlockUnpause()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;

        menuCurrentlyOpen.SetActive(isPaused);
        menuCurrentlyOpen = null;
    }

    public void checkEnemyTotal()
    {
        enemyCount--;
        //    if (enemyCount <= 0)
        //    {
        //        yield return new WaitForSeconds(2);
        //        gameOver = true;
        //        winCondition.SetActive(true);
        //        menuCurrentlyOpen = winCondition;
        //        cursorLockPause();
        //    }
    }

    public void checkDoorEnemyTotal()
    {
        doorEnemyCount--;
    }

    public void isCoreDestroyed()
    {
        if (core.activeInHierarchy == false)
        {
            if (door.activeInHierarchy == false)
            {
                door.SetActive(true);
            }
        }
    }
}