using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunPickup : MonoBehaviour
{
    [SerializeField] gunStats gunStat;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<playerController>().gunPickup(gunStat.shootRate, gunStat.shootDamage,gunStat.shootingDist, gunStat.ammo, gunStat.model, gunStat);
            Destroy(gameObject);
        }
    }
}