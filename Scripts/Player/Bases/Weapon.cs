/**
 * Script:  Weapon.cs
 * Date:    July 5
 * Author:  Ahmed Yehia
 * Description:
 *  Represents the weapon structure 
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public struct WeaponSounds
{
    public AudioClip fireAudioClip, reloadAudioClip, emptyAudioClip;
}
[Serializable]
public struct WeaponDamageSystem
{
    public float minDamage, maxDamage, shootingRange;
    public float force;
    [HideInInspector]
    public int bullets;
    public int  clips;
    public float fireRate;
    public int bulletsPerClip;
    
}
[Serializable]
public struct WeaponAnimationSystem
{
    public string draw,reload,shoot,idle;
    public float shootSpeed,reloadSpeed;
    public float reloadTime;
}
public enum ShootingMode
{
    Singleshot,
    Auto
}
public class Weapon : MonoBehaviour {

    public string weaponName;
    public GameObject weaponModel;
    public bool hasMuzzleFlash;
    public GameObject muzzleFlash;
    public bool hasHitParicles;
    public ParticleEmitter hitParticles;
    public ShootingMode shootingMode;
    public bool hasSound;
    public WeaponSounds weaponSounds;
    public WeaponDamageSystem damageSystem;
    public WeaponAnimationSystem animationSystem;
    public bool hasProjectile;
    public GameObject projectile;
    public Vector3 projectilePoint;
    public string ReticleName;
    public Vector3 ShootRay;
    public bool isReloading = false;
    public GUIContent GUIInfo = new GUIContent();
    private int _lastFrameShot;//For muzzle flash detection
    private float nextFireTime;
    [HideInInspector]
    public bool isShooting;
    public Transform shootingPoint;

    [Header("Aim Down Sights")]
    public bool enableAim;
    public float aimFieldOfView,normalFieldOfView;
    public Vector3 aimPosition, normalPosition;
    public float aimSpeed;
    public float aimRotation, normalRotation;
    public WeaponDamageSystem aimDamageSystem;
    public float aimMovementSpeed, normalMovementSpeed;
    float curField;
    float dampVel = .2f;
    public bool isAiming;
    public string aimReticleName;
    public float _minDamage, _maxDamage, _force;
    public ReticleChangerCVersion reticleChanger;
    private WeaponDamageSystem refDamageSystem;
    public Vignetting vi;
    public bool isAdrenaline;
    public bool hasDecal;
    public GameObject[] decals;
    [Header("Water")]
    public GameObject waterHitEffect;
    void Awake()
    {
        animation[animationSystem.shoot].layer = 10;
        animation[animationSystem.reload].layer = 20;
        animation[animationSystem.idle].layer = 1;
        animation[animationSystem.reload].speed = animationSystem.reloadSpeed;
    }
	void Start () {
        animation.Play(animationSystem.draw);
        if (hasHitParicles)
        {
            hitParticles.emit = false;
        }
        damageSystem.bullets = damageSystem.bulletsPerClip;
        refDamageSystem = this.damageSystem;

	}
	// Update is called once per frame
	void Update () {
        gameObject.transform.animation.Play(animationSystem.idle);
        if (Input.GetButton("Fire"))
        {
            isShooting = true;
            Fire();
        }
        else
        {
            isShooting = false;
        }
        if (Input.GetButton("Reload"))
        {
            Reload();
        }
        AimDownSights();

	}
    void LateUpdate()
    {
        if (hasMuzzleFlash)
        {
            if (_lastFrameShot == Time.frameCount)
            {
                //We shot, so enable it!
                muzzleFlash.transform.localRotation = Quaternion.AngleAxis(360 * UnityEngine.Random.value, Vector3.forward);
                muzzleFlash.SetActive(true);

                if (hasSound)
                {
                    if (!audio.isPlaying)
                    {
                        //Since MuzzleFlash is associated with high speed shot rate, eg: UZI
                        audio.loop = true;
                        audio.PlayOneShot(weaponSounds.fireAudioClip);
                    }
                }
            }

            else
            {
                muzzleFlash.SetActive(false);
                if (audio)
                {
                    audio.loop = false;
                }
            }    
        }
        else
        {
            //Just Sound
            if (_lastFrameShot == Time.frameCount)
            {
                if (hasSound)
                {
                    if (!audio.isPlaying)
                    {
                        audio.PlayOneShot(weaponSounds.fireAudioClip);
                    }
                }
            }
        }
    }
    void Fire()
    {

        if (damageSystem.bullets ==0 && damageSystem.clips >0 && isReloading == false)
        {
            Reload();
        }
 
        if (damageSystem.bullets != 0 && isReloading == false)
        {
            //Then we fire
            if (Time.time - damageSystem.fireRate > nextFireTime)
            {
                nextFireTime = Time.time - Time.deltaTime;
            }
            animation.Play(animationSystem.shoot);

            while (nextFireTime < Time.time && damageSystem.bullets != 0)
            {
                FireOneShot();
                nextFireTime += damageSystem.fireRate;
            }
        }
        else
        {
            if (damageSystem.bullets == 0)
            {
                audio.PlayOneShot(weaponSounds.emptyAudioClip);
            }
        }
    }
    void FireOneShot()
    {
        animation[animationSystem.shoot].normalizedSpeed = animationSystem.shootSpeed;
        Vector3 shootDirection = weaponModel.transform.TransformDirection(Vector3.forward);
        RaycastHit hitInfo;
        //Check Hitting
        /*Physics.Raycast(Camera.main.ScreenPointToRay(shootDirection),out hitInfo,damageSystem.shootingRange))/*/
        if (Physics.Raycast(shootingPoint.position, shootDirection, out hitInfo, damageSystem.shootingRange)) 
        {
            if (hitInfo.rigidbody)
            {
                hitInfo.rigidbody.AddForceAtPosition(damageSystem.force * shootDirection,hitInfo.point);
            }
            //Do we have hit Particles?
            if (hasHitParicles)
	        {
                hitParticles.transform.position = hitInfo.point;
                hitParticles.transform.rotation = Quaternion.FromToRotation(Vector3.up,hitInfo.normal);
                hitParticles.Emit();
	        }
            if (hitInfo.collider.transform.tag == "HitHead" || hitInfo.collider.transform.tag == "HitBody")
            {
                hitInfo.collider.transform.GetComponent<HitListener>().SendHit();
                Debug.Log("Hit");
            }
            if (hitInfo.collider.tag == "Water")
            {
                Instantiate(waterHitEffect, hitInfo.point, Quaternion.identity);
            }
            if (hitInfo.collider.tag !="Player")
	        {
                hitInfo.transform.SendMessage("GetDamage",UnityEngine.Random.Range(damageSystem.minDamage,damageSystem.maxDamage),SendMessageOptions.DontRequireReceiver);
	        }
            if (hasProjectile)
	        {
                Instantiate(projectile,transform.position,Quaternion.FromToRotation(Vector3.up,transform.position));
	        }
            if (hasDecal && hitInfo.collider.tag != "Water")
            {
                int _decalNum = UnityEngine.Random.Range(0, decals.Length - 1);
                (Instantiate(decals[_decalNum], hitInfo.point, Quaternion.FromToRotation(Vector3.up, hitInfo.normal)) as GameObject).transform.parent = hitInfo.transform;
            }
        }
        damageSystem.bullets --;
        //Register that we shot
        _lastFrameShot = Time.frameCount;
        //Update our behaviuor
        enabled = true;
    }
    IEnumerator WaitForReloadTime(float reloadTime)
    {
        Debug.Log("started");
        yield return new WaitForSeconds(reloadTime);
        damageSystem.bullets = damageSystem.bulletsPerClip;
        Debug.Log("doinn");
        isReloading = false;
    }
    void Reload()
    {
        if (damageSystem.clips > 0 && !isReloading && damageSystem.bullets != damageSystem.bulletsPerClip)
        {
            isReloading = true;
            damageSystem.clips--;
            animation.Play(animationSystem.reload);
            audio.PlayOneShot(weaponSounds.reloadAudioClip);
            StartCoroutine(WaitForReloadTime( animationSystem.reloadTime));
        }
        else
        {
            audio.PlayOneShot(weaponSounds.emptyAudioClip);
        }
        
    }
    void OnDrawGizmosSelected()
    {
        if (hasProjectile)
        {
            Gizmos.DrawIcon(transform.position + projectilePoint, "Shoot.psd", true);    
        }

    }
    void OnDrawGizmos()
    {
        Vector3 shootDirection = weaponModel.transform.TransformDirection(Vector3.forward);
        Gizmos.DrawRay(transform.position + ShootRay, shootDirection);//
    }
    void AimDownSights()
    {
        float newField = Mathf.SmoothDamp(Camera.main.fieldOfView, curField, ref dampVel, .3f); // Smoothly adjust fov
        
        Camera.main.fieldOfView = newField;

        if (Input.GetButtonDown("Fire2") && enableAim) // Are we aiming ?
            isAiming = !isAiming;

        if(isAiming)
        {
            curField = aimFieldOfView; // First set the new fov
            transform.root.GetComponent<CharacterMotor>().movement.maxForwardSpeed = aimMovementSpeed;
            this.damageSystem.maxDamage = _maxDamage;
            this.damageSystem.minDamage = _minDamage;
            this.damageSystem.force = _force;
            reticleChanger.ReticleType = aimReticleName;
            transform.localPosition = Vector3.Lerp(transform.localPosition, aimPosition, 0.3f);
            vi.enabled = true;
        }
        else if(!isAiming)
        {
            curField = normalFieldOfView; // First set the new fov
            if (!isAdrenaline)
            {
                transform.root.GetComponent<CharacterMotor>().movement.maxForwardSpeed = normalMovementSpeed;
            }
            else
            {
                transform.root.GetComponent<CharacterMotor>().movement.maxForwardSpeed = transform.root.GetComponent<Player>()._playerHealthSystem.maxAdrSpeed;

            }
            this.damageSystem.minDamage = refDamageSystem.minDamage;
            this.damageSystem.maxDamage = refDamageSystem.maxDamage;
            this.damageSystem.force = refDamageSystem.force;
            reticleChanger.ReticleType = ReticleName;
            transform.localPosition = Vector3.Lerp(transform.localPosition, normalPosition, aimSpeed);
            vi.enabled = false;
        }
        else
        {
            return;
        }
    }
    void AdrenalineChecker()
    {
        if (isAdrenaline)
        {

        }
        else
        {

        }
    }
}
