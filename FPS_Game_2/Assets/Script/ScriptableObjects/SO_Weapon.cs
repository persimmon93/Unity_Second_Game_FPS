using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptble Object/Item/Weapons", order = 2)]
[RequireComponent(typeof(WeaponClass))]
public class SO_Weapon : ScriptableObject
{
    [HeaderAttribute("Weapon Data")]
    public new string name;
    [TextArea(20, 20)]
    public string description = "Comment Here.";
    public GameObject prefab;
    public Image image;

    [HeaderAttribute("Weapon Stats")]
    [Range(10f, 60f)]
    [SerializeField] internal float damage;
    [Range(10f, 600f)]
    [SerializeField] internal float range;
    [Range(2, 7)]
    [SerializeField] internal float fireRate;  //Rate of fire. The lower it is, the slower it shoots.
    [Range(7, 60)]
    [SerializeField] internal int maxAmmoCount;
    [Range(100f, 500f)]
    [SerializeField] internal float impactForce;  //Amount gameobject will be pushed back.

    [SerializeField] internal WeaponType weapontype;

    private void OnEnable()
    {
        impactForce = damage + 100f;
        if (prefab == null)
            Debug.LogWarning("Weapon prefab missing");
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
