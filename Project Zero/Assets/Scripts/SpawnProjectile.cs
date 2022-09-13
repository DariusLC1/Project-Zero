using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectile : MonoBehaviour
{
    public GameObject projectile;
    public List<GameObject> effects = new List<GameObject>();

    //Quaternion rot = Quaternion.LookRotation(Vector3.forward * shootingDist + deviation3D);
    //Vector3 forwardVector = Camera.main.transform.rotation * rot * Vector3.forward;


    public Camera cam;

    private GameObject effectProjectile;
    void Start()
    {
        effectProjectile = effects[0]; 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Shoot"))
        {
            SpawnEffects();
        }
    }

    void SpawnEffects()
    {
        GameObject effect;
        if(projectile != null)
        {
            effect = Instantiate(effectProjectile, projectile.transform.position, Quaternion.identity);
            if(cam != null)
            {
                effect.transform.localRotation = cam.transform.localRotation;
            }
        }
        else
        {
            Debug.Log("No Fire");
        }
    }
}
