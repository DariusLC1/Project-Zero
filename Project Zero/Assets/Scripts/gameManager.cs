using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    public GameObject player;
    public playerController playerScript;

    public GameObject pauseMenu;
    public GameObject menuCurrentlyOpen;
    public GameObject playerDeadMenu;
    public GameObject playerDamageFlash;

    public GameObject playerSpawnPos;

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
        if (Input.GetButtonDown("Cancel") )
        {
            isPaused = !isPaused;
            menuCurrentlyOpen = pauseMenu;
            menuCurrentlyOpen.SetActive(isPaused);

            if (isPaused)
                cursorLockPause();
            else
                cursorUnlockUnpause();
        }
    }

    public void cursorLockPause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;

    }

    public void cursorUnlockUnpause()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;

        menuCurrentlyOpen.SetActive(isPaused);
        menuCurrentlyOpen = null;
    }


}
