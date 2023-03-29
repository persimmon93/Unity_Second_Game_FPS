using FPS_Game;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script will find the tag of the game object that contains this script and attribute data
/// according to the tag. 
/// It will also contain an attack function necessary for a weapon.
/// For now this is a generic weapons script. For special weapons, will need to make separate classes for weapons.
/// </summary>

public class GunClass : MonoBehaviour, IPickUpAble
{
    [SerializeField] public GunScriptableObject gunScriptObject;

    //[HeaderAttribute("Weapon Data Inherited from Scriptable Object")]
    internal new string name = "Gun";
    internal new string description = "This is a gun.";
    internal Sprite gunSprite;
    internal float damage = 10f;
    internal float range = 100f;
    internal float fireRate = 5f;
    internal int maxAmmoCount = 7;
    //Index 0 should be shooting and index 1 should be reloading.
    internal AudioClip[] gunAudio;

    //[HeaderAttribute("Weapon Data Not from Scriptable Object")]
    internal ParticleSystem muzzleFlash;
    internal float impactForce;
    [SerializeField] internal int currentAmmoCount;
    internal float nextTimeToFire;
    internal Interactable itemPickUp;

    internal AudioSource audioSource;
    private void OnEnable()
    {
        if (gunScriptObject != null)
        {
            name = gunScriptObject.name;
            description = gunScriptObject.description;
            gunSprite = gunScriptObject.sprite;
            damage = gunScriptObject.damage;
            range = gunScriptObject.range;
            fireRate = gunScriptObject.fireRate;
            maxAmmoCount = gunScriptObject.maxAmmoCount;
            gunAudio = gunScriptObject.gunAudio;
        }
        //Add Interactable Component.
        itemPickUp = (!GetComponent<Interactable>()) ? gameObject.AddComponent<Interactable>() : GetComponent<Interactable>();


        impactForce = (damage / 10) * 100;
        muzzleFlash = GetComponentInChildren<ParticleSystem>();

        audioSource = (gameObject.GetComponent<AudioSource>() == null) ? gameObject.AddComponent<AudioSource>() : GetComponent<AudioSource>();
        nextTimeToFire = 0f;
    }

    private void Update()
    {

    }

    public void PrimaryUse()
    {
        if (currentAmmoCount <= 0)
            return;

        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + (1f / fireRate);
            currentAmmoCount--;

            //Play ParticleEffects here.
            if (muzzleFlash != null)
            {
                muzzleFlash.Play();
            }

            //Play Audio Here.
            if (gunAudio[0] != null)
            {
                audioSource.clip = gunAudio[0];
                audioSource.Play();
            }

            if (Physics.Raycast(muzzleFlash.transform.position,
                -muzzleFlash.transform.forward, //MuzzleFlash is inversed
                out RaycastHit hit,
                gunScriptObject.range))
            {
                Debug.DrawRay(muzzleFlash.transform.position, muzzleFlash.transform.forward * range, Color.black);
                if (hit.transform.GetComponent<HitEffect>())
                {
                    GameObject hitEffect = Instantiate(hit.transform.GetComponent<HitEffect>().hitEffect,
                            hit.point,
                            Quaternion.LookRotation(hit.normal),
                            hit.transform);
                    Destroy(hitEffect, 1f);
                }

                if (hit.transform.GetComponent<HealthClass>())
                {
                    hit.transform.GetComponent<HealthClass>().ChangeHealth(-damage);
                }

                if (hit.transform.GetComponent<Rigidbody>())
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }
            }
        }
    }

    /// <summary>
    /// Ammo is the total amount of ammo in inventory.
    /// </summary>
    /// <param name="ammo"></param>
    /// <returns></returns>
    public int Reload(int ammo)
    {
        //Run animation here.



        //Run Audio here.
        if (gunAudio[1] != null)
        {
            audioSource.clip = gunAudio[1];
            audioSource.Play();
        }

        //Calculate how much ammo is required.
        int requiredAmmo = gunScriptObject.maxAmmoCount - currentAmmoCount;
        if (requiredAmmo == 0)
            return ammo;

        if (ammo > requiredAmmo)
        {
            ammo -= requiredAmmo;
            currentAmmoCount += requiredAmmo;
        }
        else
        {
            currentAmmoCount += ammo;
            ammo = 0;   //All ammo will be put in gun.
        }
        return ammo;
    }

    private void Drop()
    {

    }

    public GameObject PickUp()
    {
        return gameObject;
    }
    //public void Shoot()
    //{
    //    gunScriptObject.muzzleFlash.Play();
    //    //Play Audio here.

    //    if (Physics.Raycast(gunScriptObject.muzzleFlash.transform.position,
    //        gunScriptObject.muzzleFlash.transform.forward,
    //        out RaycastHit hit,
    //        gunScriptObject.range))
    //    {
    //        if (hit.transform.GetComponent<HitEffect>())
    //        {
    //            Debug.Log("hit effect!");
    //            GameObject hitEffect = Instantiate(hit.transform.GetComponent<HitEffect>().hitEffect,
    //                    hit.point,
    //                    Quaternion.LookRotation(hit.normal),
    //                    hit.transform);
    //            Destroy(hitEffect, 1f);
    //        }

    //        if (hit.transform.GetComponent<HealthClass>())
    //        {
    //            hit.transform.GetComponent<HealthClass>().ChangeHealth(-gunScriptObject.damage);
    //        }

    //        if (hit.transform.GetComponent<Rigidbody>())
    //        {
    //            hit.rigidbody.AddForce(-hit.normal * gunScriptObject.impactForce);
    //        }
    //    }
    //}


    /// <summary>
    /// This is the function that enables weapons to be used to attack.
    /// </summary>
    /// <param name="origin"></param> 
    /// Transform for where the raycast will shoot out from. For player
    /// this sill from camera. For NPCs, this will have a different transform.
    //internal void Attack(Ray ray)
    //{
    //    //muzzleFlash.Stop();
    //    if (gunScriptObject.currentAmmoCount <= 0)
    //        return;

    //    if (Time.time >= gunScriptObject.nextTimeToFire)
    //    {
    //        gunScriptObject.nextTimeToFire = Time.time + 1f / gunScriptObject.fireRate;

    //        gunScriptObject.currentAmmoCount--;
    //        if (gunScriptObject.muzzleFlash != null)
    //        {
    //            gunScriptObject.muzzleFlash.Play();
    //        }
    //        //Play Audio here.


    //        //This should only run if raycast hits tag.
    //        //Add accuracy implementation later.
    //        if (Physics.Raycast(ray, out RaycastHit hit, gunScriptObject.range))
    //        {
    //            ////If target is null, it is an environment.
    //            //if (hit.transform.GetComponent<Rigidbody>())
    //            //{
    //            //    hit.rigidbody.AddForce(-hit.normal * gunScriptObject.impactForce);
    //            //}

    //            //if (hit.transform.GetComponent<HealthClass>())
    //            //{
    //            //    hit.transform.GetComponent<HealthClass>().ChangeHealth(-gunScriptObject.damage);
    //            //    UIManager.Instance.uiTarget.ChangeHealth(-gunScriptObject.damage);
    //            //}

    //            //if (hit.transform.GetComponent<MainClass_NPC>().hitEffect != null)
    //            //{
    //            //    GameObject hitEffect = Instantiate(hit.transform.GetComponent<MainClass_NPC>().hitEffect,
    //            //        hit.point,
    //            //        Quaternion.LookRotation(hit.normal),
    //            //        transform);
    //            //    Destroy(hitEffect, 1f);
    //            //}
    //        }
    //    }
    //}

    //internal int Reload(int ammo)
    //{
    //    int returnAmmo = (ammo > gunScriptObject.maxAmmoCount) ? ammo - gunScriptObject.maxAmmoCount : 0;
    //    //This should depend on amount of ammo in inventory.
    //    currentAmmoCount += Mathf.Clamp(ammo, 0, gunScriptObject.maxAmmoCount);
    //    return returnAmmo;
    //}




    //=========Get Methods=========

    //public string GetName()
    //{
    //    return name;
    //}

    //public string GetDescription()
    //{
    //    return description;
    //}

    //public int GetMaxAmmo()
    //{
    //    return maxAmmoCount;
    //}
}
