using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour, IDamageable
{
    [Header("----- Components -----")]
    [SerializeField] CharacterController controller;

    [Header("----- Player Stats -----")]
    [Range(0, 100)] public int HP;
    [Range(1, 10)][SerializeField] float playerSpeed;
    [Range(1, 4)][SerializeField] float sprintMultiplyer;
    [Range(8, 18)][SerializeField] float jumpHeight;
    [Range(15, 30)][SerializeField] float gravity;
    [Range(1, 3)][SerializeField] int jumpsMax;

    [Header("----- Weapon Stats -----")]
    [Range(1, 200)][SerializeField] int shootingDist;
    [Range(.01f, 200)][SerializeField] float shootRate;
    [Range(.01f, 200)][SerializeField] int shootDamage;
    //[Range(.01f, 200)][SerializeField] int bulletPershot;
    [SerializeField] int ammoCount;
    [SerializeField] GameObject gunModel;
    public List<gunStats> gunStat = new List<gunStats>();

    [Header("----- Effects -----")]
    [SerializeField] GameObject hitEffect;

    [Header("----- Weapon Sounds -----")]
    [SerializeField] AudioSource aud;
    [SerializeField] AudioClip[] soundDamage;
    [Range(0, 50)][SerializeField] float soundDamageVol;
    [SerializeField] AudioClip[] shootSound;
    [Range(0, 50)][SerializeField] float shootSoundVol;
    [SerializeField] AudioClip[] footSteps;
    [Range(0, 50)][SerializeField] float footStepsVol;

    private Vector3 playerVelocity;
    Vector3 move = Vector3.zero;
    int timesJumped;
    float playerSpeedOriginal;
    bool isSprinting = false;
    int HPOrig;
    bool isShooting = false;
    int amtWeapon = 0;
    int ammoCountOg;


    // Start is called before the first frame update
    void Start()
    {
        playerSpeedOriginal = playerSpeed;
        HPOrig = HP;
        ammoCountOg = ammoCount;
        updatePlayerHP();
        updateAmmoCount();
        
    }

    // Update is called once per frame
    void Update()
    {

        weaponSwap();
        playerMovement();
        Sprint();
        reload();
        StartCoroutine(shoot());
        

    }
    #region PlayerStuff
    void playerMovement()
    {
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            timesJumped = 0;
        }

        //get input from Unity input system
        move = (transform.right * Input.GetAxis("Horizontal")) + (transform.forward * Input.GetAxis("Vertical"));

        // add our move vector to character controller
        controller.Move(move * Time.deltaTime * playerSpeed);

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && timesJumped < jumpsMax)
        {
            playerVelocity.y = jumpHeight;
            timesJumped++;
        }

        playerVelocity.y -= gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
 
    void Sprint()
    {
        if (Input.GetButtonDown("Sprint"))
        {
            isSprinting = true;
            playerSpeed = playerSpeed * sprintMultiplyer;
        }
        if (Input.GetButtonUp("Sprint"))
        {
            isSprinting = false;
            playerSpeed = playerSpeedOriginal;
        }
    }

    IEnumerator damageFlash()
    {
        gameManager.instance.playerDamageFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        gameManager.instance.playerDamageFlash.SetActive(false);
    }

    public void resetHP()
    {
        HP = HPOrig;
        updatePlayerHP();
    }

    public void respawn()
    {
        controller.enabled = false;
        transform.position = gameManager.instance.playerSpawnPos.transform.position;
        controller.enabled = true;
    }

    public void takeDamage(int dmg)
    {
        HP -= dmg;

        //aud.PlayOneShot(soundDamage[Random.Range(0, soundDamage.Length)], soundDamageVol);

        updatePlayerHP();
        StartCoroutine(damageFlash());

        if (HP <= 0)
        {
            death();
        }
    }

    public void death()
    {
        gameManager.instance.cursorLockPause();
        gameManager.instance.menuCurrentlyOpen = gameManager.instance.playerDeadMenu;
        gameManager.instance.menuCurrentlyOpen.SetActive(true);
    }
    #endregion
    #region Sounds
    IEnumerator footsteps()
    {
        //if (controller.isGrounded && move.normalized.magnitude > 0.3f)
        //{
        //    playerFootStep = false;
        //    aud.PlayOneShot(footSteps[Random.Range(0, footSteps.Length)], footStepsVol);
        //    if (!isSprinting)
        //    {
        //        yield return new WaitForSeconds(0.4f);
        //    }
        //    else
        //        yield return new WaitForSeconds(0.4f);

        //    playerFootStep = true;
        //}
        yield return null; // change this when you're done
    }
    #endregion
    #region Weapons
    // --------- for Weapons ------------ \\
    IEnumerator shoot()
    {

        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * shootingDist, Color.red, 0.00001f);
        if (gunStat.Count != 0 && Input.GetButton("Shoot") && isShooting == false)
        {
            isShooting = true;
            ammoCount--;
            updateAmmoCount();
            

            // do something
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, shootingDist))
            {
                if (hit.collider.GetComponent<IDamageable>() != null)
                {
                    IDamageable isDamageable = hit.collider.GetComponent<IDamageable>();

                    if (hit.collider is SphereCollider)
                        isDamageable.takeDamage(shootDamage * 2);
                    
                    else
                        isDamageable.takeDamage(shootDamage);
                        gameManager.instance.isCoreDestroyed();

                }
            }

            yield return new WaitForSeconds(shootRate);
            isShooting = false;
        }
    }

    public void gunPickup(float shtRate, int shtingDist, int shtDamage, int ammo,  GameObject model,gunStats _gstats)
    {
        shootRate = shtRate;
        shootingDist = shtingDist;
        shootDamage = shtDamage;
        gunModel.GetComponent<MeshFilter>().sharedMesh = model.GetComponent<MeshFilter>().sharedMesh;
        gunModel.GetComponent<MeshRenderer>().sharedMaterial = model.GetComponent<MeshRenderer>().sharedMaterial;
        ammoCount = ammo;
        ammoCountOg = ammoCount;
        gunStat.Add(_gstats);
        updateAmmoCount();
    }

    void weaponSwap()
    {
        if (gunStat.Count > 0)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0 && amtWeapon < gunStat.Count - 1)
            {
                amtWeapon++;
                shootRate = gunStat[amtWeapon].shootRate;
                shootingDist = gunStat[amtWeapon].shootingDist;
                shootDamage = gunStat[amtWeapon].shootDamage;
                gunModel.GetComponent<MeshFilter>().sharedMesh = gunStat[amtWeapon].model.GetComponent<MeshFilter>().sharedMesh;
                gunModel.GetComponent<MeshRenderer>().sharedMaterial = gunStat[amtWeapon].model.GetComponent<MeshRenderer>().sharedMaterial;
                updateAmmoCount();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0 && amtWeapon > 0)
            {
                amtWeapon--;
                shootRate = gunStat[amtWeapon].shootRate;
                shootingDist = gunStat[amtWeapon].shootingDist;
                shootDamage = gunStat[amtWeapon].shootDamage;
                gunModel.GetComponent<MeshFilter>().sharedMesh = gunStat[amtWeapon].model.GetComponent<MeshFilter>().sharedMesh;
                gunModel.GetComponent<MeshRenderer>().sharedMaterial = gunStat[amtWeapon].model.GetComponent<MeshRenderer>().sharedMaterial;
                updateAmmoCount();
            }
        }
    }

    public void updatePlayerHP()
    {
        gameManager.instance.playerHPBar.fillAmount = (float)HP / (float)HPOrig;
    }

    public void updateAmmoCount()
    {
        gameManager.instance.ammoCount.fillAmount = (float)ammoCount / (float)ammoCountOg;
    }

    void reload()
    {
        if (Input.GetButtonDown("Reload"))
        {
            ammoCount = ammoCountOg;
            updateAmmoCount();
        }
    }
    #endregion


}
