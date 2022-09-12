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
            other.GetComponent<playerController>().gunPickup(gunStat.shootRate, gunStat.shootingDist, gunStat.shootDamage, gunStat.ammo, gunStat.ammoLeft, gunStat.model,
                gunStat.shootSpread, gunStat.shootingSound, gunStat.shootingVol, gunStat.emplyClick, gunStat.emptyClickVol, gunStat.reloadSound, gunStat.reloadSoundVol, gunStat.reloadTime, gunStat);
            Destroy(gameObject);
        }
    }
}