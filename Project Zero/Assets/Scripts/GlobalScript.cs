using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScript : MonoBehaviour
{
    public static GlobalScript Instance;
    public playerdata savedPlayerData = new playerdata();

    public int GMaxammoCount = 0;
    public bool hassheild;
    public bool haswalljump;
    public int amtWeapon = 0;
    public bool bossRush;
    public int scenes;
    public List<gunStats> GlobalgunStat = new List<gunStats>();
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Debug.Log("worked");
        if (GlobalgunStat.Count != 0)
        {
            LoadPlayer();
        }
    }
    public void LoadPlayer()
    {
        gameManager.instance.playerScript.amtWeapon = amtWeapon;
        gameManager.instance.playerScript.hassheild = hassheild;
        gameManager.instance.playerScript.haswalljump = haswalljump;
        gameManager.instance.playerScript.MaxammoCount = GMaxammoCount;
        for (int i = 0; i < GlobalgunStat.Count; i++)
        {
            gameManager.instance.playerScript.gunStat.Add(GlobalgunStat[i]);
        }

    }
}

