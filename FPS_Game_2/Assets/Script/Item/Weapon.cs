using UnityEngine;

/// <summary>
/// This script will find the tag of the game object that contains this script and attribute data
/// according to the tag. 
/// It will also contain an attack function necessary for a weapon.
/// For now this is a generic weapons script. For special weapons, will need to make separate classes for weapons.
/// </summary>

public class Weapon : WeaponSO
{
    private int currentAmmoCount;
    private float nextTimeToFire;
    public GameObject hitEffect;

    // Start is called before the first frame update
    void Start()
    {
        nextTimeToFire = 0f;
    }

    // Update is called once per frame
    void Update()
    {
            //switch (s)
            //{
            //    case "Pistol":
            //        damage = 20f;
            //        range = 100f;
            //        maxAmmoCount = 7;
            //        currentAmmoCount = maxAmmoCount;
            //        fireRate = 2f;
            //        impactForce = 100f;
            //        break;
            //    case "Rifle":
            //        damage = 50f;
            //        range = 200f;
            //        maxAmmoCount = 30;
            //        currentAmmoCount = maxAmmoCount;
            //        fireRate = 6f;
            //        impactForce = 200f;
            //        break;
            //    case "Sniper":
            //        damage = 100f;
            //        range = 300f;
            //        maxAmmoCount = 5;
            //        currentAmmoCount = maxAmmoCount;
            //        fireRate = 4f;
            //        impactForce = 300f;
            //        break;
            //    case "Shotgun":
            //        damage = 200;
            //        range = 50;
            //        maxAmmoCount = 6;
            //        currentAmmoCount = maxAmmoCount;
            //        fireRate = 3f;
            //        impactForce = 250f;
            //        break;
            //    default:
            //        //Melee Attack (may need to disable this or make a separate class for melee. Because melee should
            //        //not have ammo and it messes with the Attack() function.)
            //        damage = 10f;
            //        range = 5f;
            //        muzzleFlash = null;
            //        impactForce = 50f;
            //        fireRate = 2;
            //        break;
            //}
            //weaponIdentity = true;
        //}
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
        //And fire depending on fire rate of gun.
        if (currentAmmoCount > 0 && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            //This should run regardless of whether raycast hits something.
            if (muzzleFlash != null)
            {
                muzzleFlash.Play();
                currentAmmoCount--;
            }

            //This should only run if raycast hits something.
            RaycastHit hit;
            if (Physics.Raycast(origin.position, origin.forward, out hit, range))
            {
                HealthClass target = hit.transform.GetComponent<HealthClass>();
                //If target is null, it is an environment.
                if (target != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                    target.SetMaxHealth(damage);
                }


                //Creates gameobject with the effect of bullet impact.
                //Possible future idea is to add an array of different hitEffects for flesh, wood, metal effects.
                GameObject impactGO = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 1f);
            }
        }
    }

    internal void Reload()
    {
        //if (canReload)
        //{
        //    currentAmmoCount = maxAmmoCount;
        //}
    }
}
