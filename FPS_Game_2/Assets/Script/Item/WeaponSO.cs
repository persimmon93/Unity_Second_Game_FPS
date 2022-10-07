using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Data", menuName = "Data/Weapon Data")]
public class WeaponSO : ScriptableObject
{
    public string itemName;

    public int damage;
    public int range;
    [Range(2, 7)]
    public float fireRate;  //Rate of fire. The lower it is, the slower it shoots.
    public int maxAmmoCount;
    internal float impactForce;  //Amount gameobject will be pushed back.

    public WeaponType weapontype;
    public GameObject itemPrefab;

    public ParticleSystem muzzleFlash;    //Already set in prefab.

    void Start()
    {
        impactForce = damage + 100f;
    }
}
public enum WeaponType
{
    Pistol,
    Rifle,
    Sniper,
    Shotgun,
    Grenade,
    Knife
} 
