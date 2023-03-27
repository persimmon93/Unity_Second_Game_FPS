using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryClass : MonoBehaviour
{
    public GameObject currentEquippedItem;
    private Dictionary<Transform, int> inventory = new Dictionary<Transform, int>();
    private Dictionary<WeaponType, int> ammoInventory = new Dictionary<WeaponType, int>();
    [SerializeField] private Transform itemPlacer;    //Transform where the player/npc will hold items.

    private void OnEnable()
    {
        if (itemPlacer == null)
            Debug.LogWarning("ItemPlacer reference missing for inventory class. Will cause error when picking up item.");
    }


    public void ItemPlacer(GameObject item)
    {
        if (!item)
        {
            
        }
    }

    public void RemoveItem(Transform item)
    {
        if (!inventory.ContainsKey(item))
            return;
    }
    public void ChangeInventory(Transform item, bool addItem)
    {
        //If bool is true, add item. If false, remove item.
        if (inventory.ContainsKey(item))
        {
            //inventory = addItem ? inventory.Add(item, 1) : inventory.Remove(item, 1);
        }
    }

    public void ChangeAmmoValue(WeaponType weaponType, int ammo)
    {
        ammoInventory[weaponType] += ammo;
    }

    public int GetAmmoCount(WeaponType weaponType)
    {
        return ammoInventory[weaponType];
    }
}
