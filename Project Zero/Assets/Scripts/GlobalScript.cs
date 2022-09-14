using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScript : MonoBehaviour
{
    public static GlobalScript Instance;

    public int GHP;
    public int GMaxammoCount;
    public bool hassheild;
    public bool haswalljump;
    public int amtWeapon;
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
}
