using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectDamageable : MonoBehaviour , IDamageable
{
    [SerializeField] int HP;

    public void takeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
