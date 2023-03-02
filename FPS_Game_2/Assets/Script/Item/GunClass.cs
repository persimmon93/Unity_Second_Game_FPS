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

public class GunClass : MonoBehaviour
{
    [SerializeField] public GunScriptableObject gunScriptObject;

    //[HeaderAttribute("Weapon Data Inherited from Scriptable Object")]
    [SerializeField] internal string name;
    [SerializeField] internal string description;
    [SerializeField] internal Sprite gunSprite;
    [SerializeField] internal float damage;
    [SerializeField] internal float range;
    [SerializeField] internal float fireRate;
    [SerializeField] internal int maxAmmoCount;
    [SerializeField] internal float impactForce;
    [SerializeField] internal ParticleSystem muzzleFlash;

    //[HeaderAttribute("Weapon Data Not from Scriptable Object")]
    [SerializeField] internal int currentAmmoCount;
    [SerializeField] internal float nextTimeToFire;

    private void OnEnable()
    {
        if (gunScriptObject == null)
        {
            Debug.LogWarning("GunClass is missing a GunScriptableObject reference!");
            return;
        }
        name = gunScriptObject.name;
        description = gunScriptObject.description;
        gunSprite = gunScriptObject.sprite;
        damage = gunScriptObject.damage;
        range = gunScriptObject.range;
        fireRate = gunScriptObject.fireRate;
        maxAmmoCount = gunScriptObject.maxAmmoCount;
        impactForce = (damage / 10) * 100;
        muzzleFlash = GetComponentInChildren<ParticleSystem>();

        nextTimeToFire = 0f;
    }

    private void Update()
    {

    }

    public void Shoot()
    {
        if (currentAmmoCount <= 0)
            return;

        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + (1f / fireRate);
            currentAmmoCount--;

            muzzleFlash.Play();
            //Play Audio here.

            if (Physics.Raycast(muzzleFlash.transform.position,
                -muzzleFlash.transform.forward, //MuzzleFlash is inversed
                out RaycastHit hit,
                gunScriptObject.range))
            {
                Debug.DrawRay(muzzleFlash.transform.position, -muzzleFlash.transform.forward * 300f, Color.black);
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

    //Ammo is the total amount of ammo in inventory.
    public int Reload(int ammo)
    {
        //Run animation here.




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
            currentAmmoCount = ammo;
            ammo = 0;   //All ammo will be put in gun.
        }
        return ammo;
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
