using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptble Object/Item/Weapons", order = 2)]
public class GunScriptableObject : ScriptableObject
{
    [HeaderAttribute("Weapon Data")]
    public new string name;
    [TextArea(10, 10)]
    public string description = "Comment Here.";
    public GameObject prefab;
    public Sprite sprite;

    [HeaderAttribute("Weapon Stats")]
    [SerializeField] public float damage;
    [SerializeField] public float range;    //Bullet range after fired.
    [Range(0.1f, 15f)]
    [SerializeField] public float fireRate;  //Rate of fire. The lower it is, the slower it shoots.
    [SerializeField] public int maxAmmoCount;

    [SerializeField] public ParticleSystem muzzleFlash;


    [SerializeField] public ItemPickUp itemPickUp;

    [SerializeField] public WeaponType weapontype;

    private void OnEnable()
    {
        //Check prefab
        if (prefab == null)
        {
            Debug.LogWarning("Gun prefab is missing!");
            return;
        }
        itemPickUp = (!prefab.GetComponent<ItemPickUp>()) ? prefab.AddComponent<ItemPickUp>() : prefab.GetComponent<ItemPickUp>();

        var particleSystemMain = muzzleFlash.main;
        particleSystemMain.loop = true;
    }
}

public enum WeaponType
{
    Pistol,
    Rifle,
    Sniper,
    Shotgun
} 
