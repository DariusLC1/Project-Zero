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
    [Range(15, 30)][SerializeField] public float gravity;
    [Range(1, 3)][SerializeField] public int jumpsMax;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashTime;
    [SerializeField] float dashLength;
    public GameObject playerShield;
    [SerializeField] AudioSource aud;
    [SerializeField] AudioClip hurtSound;
    [SerializeField] float hurtSoundVol;
    

    [Header("----- Shield Stats -----")]
    [Range(1, 20)][SerializeField] int shieldCharge;
    [SerializeField] int shieldChargeOG;


    [Header("----- Weapon Stats -----")]
    [Range(1, 200)][SerializeField] int shootingDist;
    [Range(.01f, 200)][SerializeField] float shootRate;
    [Range(.01f, 200)][SerializeField] int shootDamage;
    [Range(.01f, 200)][SerializeField] int reloadTime;
    //[Range(.01f, 200)][SerializeField] int bulletPershot;
    [Range(.01f, 200)] public float ShootSpread;
    [SerializeField] int ammoCount;
    public int MaxammoCount;
    public int AmmoLeft;
    [SerializeField] GameObject gunModel;
    public List<gunStats> gunStat = new List<gunStats>();

    [Header("----- Effects -----")]
    [SerializeField] GameObject hitEffect;

    [Header("----- Weapon Sounds -----")]
    [SerializeField] AudioSource audSource;
    [SerializeField] AudioClip shootSound;
    [SerializeField] AudioClip emptyClick;
    [SerializeField] AudioClip reloadSound;
    [Range(0, 50)][SerializeField] float shootSoundVol;
    [Range(0, 50)][SerializeField] float emptyClickVol;
    [Range(0, 50)][SerializeField] float reloadSoundVol;

    private Vector3 playerVelocity;
    Vector3 move = Vector3.zero;
    int shots;
    public int timesJumped;
    float playerSpeedOriginal;
    bool isSprinting = false;
    public bool isDashable = true;
    int HPOrig;
    public bool isShooting = false;
    int amtWeapon = 0;
    int ammoCountOg;
    [SerializeField]bool isReloading;
    [SerializeField]bool canSwap = true;

    // Start is called before the first frame update
    void Start()
    {
        playerSpeedOriginal = playerSpeed;
        HPOrig = HP;
        shieldChargeOG = shieldCharge;
        ammoCountOg = ammoCount;

        audSource = GameObject.Find("Gun Model").GetComponent<AudioSource>();
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
        StartCoroutine(reload());
        StartCoroutine(Shielding());

    }
    #region PlayerStuff
    public void playerMovement()
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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(Dash());
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

    IEnumerator Dash()
    {
        isDashable = false;
        float startTime = Time.time; // need to remember this to know how long to dash
        while (Time.time < startTime + dashTime)
        {
            Vector3 moveDirection = transform.forward * dashLength;
           // controller.Move(moveDirection * dashSpeed * Time.deltaTime);
            controller.Move(moveDirection * dashSpeed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(1);
        isDashable = true;
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

        aud.PlayOneShot(hurtSound, hurtSoundVol);

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
        Vector3 shootDirection = Camera.main.transform.position;
        shootDirection.x = Random.Range(-ShootSpread, ShootSpread);
        shootDirection.y = Random.Range(-ShootSpread, ShootSpread);

        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * shootingDist, Color.red, 0.00001f);
        if (gunStat.Count != 0 && Input.GetButton("Shoot") && isShooting == false)
        {
            isShooting = true;
            if (ammoCount == 0)
                audSource.PlayOneShot(emptyClick, emptyClickVol);
            else if (isReloading == true)
            {

            }
            else
            {
                ammoCount--;
                AmmoLeft--;
                updateAmmoCount();
                audSource.PlayOneShot(shootSound, shootSoundVol);
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, shootingDist))
                {
                    Instantiate(hitEffect, hit.point, hitEffect.transform.rotation);
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
                shots++;
                gameManager.instance.ShotsFired.text = $"Shots Fired = {shots}";
            }
        }
    }

    public void gunPickup(float shtRate, int shtingDist, int shtDamage, int ammo, int ammoLeft, GameObject model, float spread, AudioClip shootingSound, float shootingVol, AudioClip emptyClip, float emptyClipVol, AudioClip reloading, float reloadingVol, int rTime, gunStats _gstats)
    {
        if (gunStat.Count == 0)
        {
            shootRate = shtRate;
s            shootingDist = shtingDist;
            shootDamage = shtDamage;
            gunModel.GetComponent<MeshFilter>().sharedMesh = model.GetComponent<MeshFilter>().sharedMesh;
            gunModel.GetComponent<MeshRenderer>().sharedMaterial = model.GetComponent<MeshRenderer>().sharedMaterial;
            ammoCount = ammo;
            ammoCountOg = ammoCount;
            MaxammoCount = ammoCountOg * 3;
            ammoLeft = ammo;
            AmmoLeft = ammoLeft;

            ShootSpread = spread;

            shootSound = shootingSound;
            shootSoundVol = shootingVol;
            emptyClick = emptyClip;
            emptyClickVol = emptyClipVol;
            reloadSound = reloading;
            reloadSoundVol = reloadingVol;

            reloadTime = rTime;

            gunStat.Add(_gstats);
            updateAmmoCount();
        }
        else if (gunStat.Count > 0)
        {
            shootRate = shtRate;
            shootingDist = shtingDist;
            shootDamage = shtDamage;
            gunModel.GetComponent<MeshFilter>().sharedMesh = model.GetComponent<MeshFilter>().sharedMesh;
            gunModel.GetComponent<MeshRenderer>().sharedMaterial = model.GetComponent<MeshRenderer>().sharedMaterial;
            ammoCount = ammo;
            ammoCountOg = ammoCount;
            ammoLeft = ammo;
            AmmoLeft = ammoLeft;
            MaxammoCount += ammoCountOg * 2;

            ShootSpread = spread;

            shootSound = shootingSound;
            shootSoundVol = shootingVol;
            emptyClick = emptyClip;
            emptyClickVol = emptyClipVol;
            reloadSound = reloading;
            reloadSoundVol = reloadingVol;

            reloadTime = rTime;

            gunStat.Add(_gstats);
            updateAmmoCount();
        }
    }

    void weaponSwap()
    {
        if (gunStat.Count > 0 && canSwap == true)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0 && amtWeapon < gunStat.Count - 1)
            {
                gunStat[amtWeapon].ammoLeft = AmmoLeft;
                amtWeapon++;
                shootRate = gunStat[amtWeapon].shootRate;
                shootingDist = gunStat[amtWeapon].shootingDist;
                shootDamage = gunStat[amtWeapon].shootDamage;
                AmmoLeft = gunStat[amtWeapon].ammoLeft;
                if(gunStat[amtWeapon].ammo != AmmoLeft)
                {
                    ammoCount = gunStat[amtWeapon].ammoLeft;
                }
                else
                {
                    ammoCount = gunStat[amtWeapon].ammo;
                }


                ShootSpread = gunStat[amtWeapon].shootSpread;

                shootSound = gunStat[amtWeapon].shootingSound;
                shootSoundVol = gunStat[amtWeapon].shootingVol;
                emptyClick = gunStat[amtWeapon].emplyClick;
                emptyClickVol = gunStat[amtWeapon].emptyClickVol;
                reloadSound = gunStat[amtWeapon].reloadSound;
                reloadSoundVol = gunStat[amtWeapon].reloadSoundVol;

                reloadTime = gunStat[amtWeapon].reloadTime;

                gunModel.GetComponent<MeshFilter>().sharedMesh = gunStat[amtWeapon].model.GetComponent<MeshFilter>().sharedMesh;
                gunModel.GetComponent<MeshRenderer>().sharedMaterial = gunStat[amtWeapon].model.GetComponent<MeshRenderer>().sharedMaterial;
                updateAmmoCount();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0 && amtWeapon > 0)
            {
                gunStat[amtWeapon].ammoLeft = AmmoLeft;
                amtWeapon--;
                shootRate = gunStat[amtWeapon].shootRate;
                shootingDist = gunStat[amtWeapon].shootingDist;
                shootDamage = gunStat[amtWeapon].shootDamage;
                AmmoLeft = gunStat[amtWeapon].ammoLeft;
                if (gunStat[amtWeapon].ammo != AmmoLeft)
                {
                    ammoCount = gunStat[amtWeapon].ammoLeft;
                }
                else
                {
                    ammoCount = gunStat[amtWeapon].ammo;
                }


                ShootSpread = gunStat[amtWeapon].shootSpread;

                shootSound = gunStat[amtWeapon].shootingSound;
                shootSoundVol = gunStat[amtWeapon].shootingVol;
                emptyClick = gunStat[amtWeapon].emplyClick;
                emptyClickVol = gunStat[amtWeapon].emptyClickVol;
                reloadSound = gunStat[amtWeapon].reloadSound;
                reloadSoundVol = gunStat[amtWeapon].reloadSoundVol;

                reloadTime = gunStat[amtWeapon].reloadTime;

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
        gameManager.instance.ammoCount.text = $"{ammoCount}/{MaxammoCount}";
    }

    IEnumerator reload()
    {
        if (Input.GetButtonDown("Reload"))
        {

            if (MaxammoCount <= 0)
            {
                isReloading = false;
            }
            else
            {
                canSwap = false;
                gameManager.instance.reloadUI.SetActive(true);
                isReloading = true;
                audSource.PlayOneShot(reloadSound, reloadSoundVol);
                yield return new WaitForSeconds(reloadTime);
                ammoCount = ammoCountOg;
                AmmoLeft = ammoCount;
                MaxammoCount -= ammoCountOg;
                gameManager.instance.reloadUI.SetActive(false);
                isShooting = false;
                isReloading = false;
                canSwap = true;
            }
            updateAmmoCount();
        }
    }
    #endregion
    #region Shield
    IEnumerator Shielding()
    {
        if (Input.GetButtonUp("Shield") && shieldCharge != 0)
        {
                playerShield.SetActive(true);
                yield return new WaitForSeconds(5);
                playerShield.SetActive(false);
                shieldCharge -= 1;
            
        }

    }
    #endregion

}
