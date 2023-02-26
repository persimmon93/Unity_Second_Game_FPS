using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

/// <summary>
/// This script will find the tag of the game object that contains this script and attribute data
/// according to the tag. 
/// It will also contain an attack function necessary for a weapon.
/// For now this is a generic weapons script. For special weapons, will need to make separate classes for weapons.
/// </summary>

public class WeaponClass : MonoBehaviour
{
    [SerializeField] private SO_Weapon scriptableObject;

    [HeaderAttribute("Weapon Data Inherited from Scriptable Object")]
    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private float fireRate;
    [SerializeField] private int maxAmmoCount;
    [SerializeField] private float impactForce;
    [SerializeField] internal ParticleSystem muzzleFlash;

    [HeaderAttribute("Weapon Data Not from Scriptable Object")]
    [SerializeField] private int currentAmmoCount;
    [SerializeField] private float nextTimeToFire;


    [SerializeField] protected ItemPickUp ScriptItemPickUp;

    private void OnEnable()
    {
        if (scriptableObject == null)
        {
            Debug.LogWarning("WeaponClass is missing a SO_Weapon reference!");
            return;
        }
        name = scriptableObject.name;
        description = scriptableObject.description;
        damage = scriptableObject.damage;
        range = scriptableObject.range;
        fireRate = scriptableObject.fireRate;
        maxAmmoCount = scriptableObject.maxAmmoCount;
        impactForce = scriptableObject.impactForce;
        nextTimeToFire = 0f;
        gameObject.tag = "Item";
        ScriptItemPickUp = (ScriptItemPickUp == null) ? gameObject.AddComponent<ItemPickUp>() : GetComponent<ItemPickUp>();
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
        var particleSystemMain = muzzleFlash.main;
        particleSystemMain.loop = false;
    }


    /// <summary>
    /// This is the function that enables weapons to be used to attack.
    /// </summary>
    /// <param name="origin"></param> 
    /// Transform for where the raycast will shoot out from. For player
    /// this sill from camera. For NPCs, this will have a different transform.
    internal void Attack(Ray ray)
    {
        //muzzleFlash.Stop();
        if (currentAmmoCount <= 0)
            return;

        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;

            currentAmmoCount--;
            if (muzzleFlash != null)
            {
                muzzleFlash.Play();
            }
            //Play Audio here.


            //This should only run if raycast hits tag.
            //Add accuracy implementation later.
            if (Physics.Raycast(ray, out RaycastHit hit, range))
            {
                //If target is null, it is an environment.
                if (hit.transform.GetComponent<Rigidbody>())
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }

                if (hit.transform.GetComponent<HealthClass>())
                {
                    hit.transform.GetComponent<HealthClass>().ChangeHealth(-damage);
                    UI_MainHandler.Instance.uiTarget.ChangeHealth(-damage);
                }

                if (hit.transform.GetComponent<MainClass_NPC>().hitEffect != null)
                {
                    GameObject hitEffect = Instantiate(hit.transform.GetComponent<MainClass_NPC>().hitEffect,
                        hit.point,
                        Quaternion.LookRotation(hit.normal),
                        transform);
                    Destroy(hitEffect, 1f);
                }
            }
        }
    }

    internal int Reload(int ammo)
    {
        int returnAmmo = (ammo > maxAmmoCount) ? ammo - maxAmmoCount : 0;
        //This should depend on amount of ammo in inventory.
        currentAmmoCount += Mathf.Clamp(ammo, 0, maxAmmoCount);
        return returnAmmo;
    }




    //=========Get Methods=========

    public string GetName()
    {
        return name;
    }

    public string GetDescription()
    {
        return description;
    }

    public int GetAmmoCount()
    {
        return currentAmmoCount;
    }

    public int GetMaxAmmo()
    {
        return maxAmmoCount;
    }
}
