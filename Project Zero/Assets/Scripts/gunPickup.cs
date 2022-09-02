using UnityEngine;

public class gunPickup : MonoBehaviour
{
    [SerializeField] gunStats gunStat;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<playerController>().gunPickup(gunStat.shootRate, gunStat.shootDamage, gunStat.shootingDist, gunStat.ammo, gunStat.MaxAmmo, gunStat.model,
                gunStat.recoilAmountX, gunStat.recoilAmountY, gunStat.shootingSound, gunStat.shootingVol, gunStat.emplyClick, gunStat.emptyClickVol, gunStat.reloadSound, gunStat.reloadSoundVol, gunStat.reloadTime,gunStat);
            Destroy(gameObject);
        }
    }
}