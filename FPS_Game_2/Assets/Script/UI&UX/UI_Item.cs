using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Item : MonoBehaviour
{
    //Public ItemClass item;
    public TMP_Text itemName;
    public TMP_Text itemDescription;
    public TMP_Text itemAmmo;

    public WeaponClass weapon;   //If ray hits weapon, should set weapon.

    private void OnEnable()
    {
        ActivateUI();
    }

    private void OnDisable()
    {
        DeactivateUI();
    }

    public void ActivateUI()
    {
        itemName.gameObject.SetActive(true);
        itemDescription.gameObject.SetActive(true);
        itemAmmo.gameObject.SetActive(true);
        if (weapon != null)
        {
            itemName.text = weapon.GetName();
            itemDescription.text = weapon.GetDescription();
            itemAmmo.text = "Ammo\n" + weapon.GetAmmoCount() + " / " + weapon.GetMaxAmmo();
        }
    }

    public void DeactivateUI()
    {
        itemName.text = "";
        itemDescription.text = "";
        itemAmmo.text = "";

        weapon = null;

        itemName.gameObject.SetActive(false);
        itemDescription.gameObject.SetActive(false);
        itemAmmo.gameObject.SetActive(false);
    }
}
