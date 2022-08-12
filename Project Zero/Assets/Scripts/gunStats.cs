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
    
}   