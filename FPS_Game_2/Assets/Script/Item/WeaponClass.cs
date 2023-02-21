using System.Security.Cryptography;
using UnityEngine;

/// <summary>
/// This script will find the tag of the game object that contains this script and attribute data
/// according to the tag. 
/// It will also contain an attack function necessary for a weapon.
/// For now this is a generic weapons script. For special weapons, will need to make separate classes for weapons.
/// </summary>

public class WeaponClass : MonoBehaviour
{
    [SerializeField] private SOWeapon scriptableObject;

    [HeaderAttribute("Weapon Data Inherited from Scriptable Object")]
    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private float fireRate;
    [SerializeField] private int maxAmmoCount;
    [SerializeField] private float impactForce;

    [HeaderAttribute("Weapon Data Not from Scriptable Object")]
    [SerializeField] private int currentAmmoCount;
    [SerializeField] private float nextTimeToFire;


    private void OnEnable()
    {
        if (scriptableObject == null)
        {
            Debug.LogWarning("WeaponClass is missing a SOWeapon reference!");
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
        CreateModel();
    }

    public void CreateModel()
    {
        //Instantiate game object model if it is null.
        foreach (GameObject gameObjectModel in transform)
        {
            if (gameObjectModel.name == "Model")
            {
                //This should be if developer implemented model manually.
                return;
            }
        }
        Instantiate(scriptableObject.prefab, transform, false);
    }


    /// <summary>
    /// This is the function that enables weapons to be used to attack.
    /// </summary>
    /// <param name="origin"></param> 
    /// Transform for where the raycast will shoot out from. For player
    /// this sill from camera. For NPCs, this will have a different transform.
    internal void Attack(Transform origin)
    {
        //Results of gun attack should only be run if ammmo is above 0.
        //Fire depends on fire rate of gun.
        if (currentAmmoCount > 0 && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / scriptableObject.fireRate;
            //This should run regardless of whether raycast hits something.
            if (scriptableObject.muzzleFlash != null)
            {
                scriptableObject.muzzleFlash.Play();
                currentAmmoCount--;
            }

            //This should only run if raycast hits something.
            //Add accuracy implementation later.
            RaycastHit hit;
            if (Physics.Raycast(origin.position, origin.forward, out hit, scriptableObject.range))
            {
                HealthClass target = hit.transform.GetComponent<HealthClass>();
                //If target is null, it is an environment.
                if (target != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * scriptableObject.impactForce);
                    target.SetMaxHealth(scriptableObject.damage);
                }


                //Creates gameobject with the effect of bullet impact.
                //Possible future idea is to add an array of different hitEffects for flesh, wood, metal effects.
                GameObject impactGO = Instantiate(scriptableObject.hitEffect, hit.point,
                    Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 1f);
            }
        }
    }

    //Test
    public int Reload(int ammo)
    {
        int returnAmmo = (ammo > maxAmmoCount) ? ammo - maxAmmoCount : 0;
        //This should depend on amount of ammo in inventory.
        currentAmmoCount += Mathf.Clamp(ammo, 0, maxAmmoCount);
        return returnAmmo;
    }

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
