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
    [TextArea(5, 5)]
    public string description = "Comment Here.";
    public GameObject prefab;
    public Sprite sprite;

    [HeaderAttribute("Weapon Stats")]
    public float damage;
    public float range;    //Bullet range after fired.
    [Range(0.1f, 15f)]
    public float fireRate;  //Rate of fire. The lower it is, the slower it shoots.
    public int maxAmmoCount;

    [HeaderAttribute("Weapon Audio")]
    public AudioClip[] gunAudio;
    public WeaponType weapontype;

    private void OnEnable()
    {
        //Check prefab
        if (prefab == null)
        {
            Debug.LogWarning("Gun prefab is missing!");
            return;
        }
    }
}

public enum WeaponType
{
    Pistol,
    Rifle,
    Sniper,
    Shotgun
}
