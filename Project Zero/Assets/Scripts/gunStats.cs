using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class gunStats : ScriptableObject
{
    [Range(1, 200)] public float shootRate;
    [Range(.01f, 200)] public int shootingDist;
    [Range(.01f, 200)] public int shootDamage;

    [Header("----- Weapon Stats -----")]
    [Range(1, 10)] public int bulletPershot;
}   