using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class gunStats : ScriptableObject
{
    [Header("----- Weapon Stats -----")]
    [Range(0.01f, 200)] public float shootRate;
    [Range(5, 200)] public int shootingDist;
    [Range(1, 200)] public int shootDamage;
    [Range(1, 200)] public int reloadTime;
    [Range(.01f, 200)] public int ammo;
    [Range(.01f, 20)] public float recoilAmountX;
    [Range(.01f, 20)] public float recoilAmountY;
    public int MaxAmmo;
    public GameObject model;

    [Header("----- Weapon Sound -----")]
    [SerializeField] public AudioClip shootingSound;
    [SerializeField] public AudioClip emplyClick;
    [SerializeField] public AudioClip reloadSound;
    [Range(0, 100f)] public float shootingVol;
    [Range(0, 100f)] public float emptyClickVol;
    [Range(0, 100f)] public float reloadSoundVol;
}   