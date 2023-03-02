using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine.UI;

public class UI_PlayerInfo : MonoBehaviour
{
    [SerializeField] private GameObject weaponIcon;
    [SerializeField] private GameObject ammoIcon;

    [SerializeField] private TMP_Text weaponName;
    [SerializeField] private TMP_Text weaponAmmo;


    [SerializeField] private TMP_Text numberHealth;
    [SerializeField] private Slider playerHealthBar;

    private void Awake()
    {
        //if (weaponIcon == null)
        //    weaponIcon = GetComponentInChildren<Sprite>();
    }

    private void Start()
    {
        if (weaponIcon == null)
            Debug.LogWarning("UI_PlayerInfo is missing reference to weapon icon.");
        if (ammoIcon == null)
            Debug.LogWarning("UI_PlayerInfo is missing reference to ammo icon.");
        if (weaponName == null)
            Debug.LogError("UI_PlayerInfo is missing reference to weapon name.");
        if (weaponAmmo == null)
            Debug.LogError("UI_PlayerInfo is missing reference to weapon ammo.");

        if (numberHealth == null)
            Debug.LogError("UI_PlayerInfo is missing reference to numberHealth.");
        if (playerHealthBar == null)
            Debug.LogError("UI_PlayerInfo is missing reference to health bar slider.");
    }

    //This method will automatically run when player equip has a weapon.
    public void DisplayWeapon(GunClass passedWeapon)
    {
        if (passedWeapon != null)
        {
            weaponIcon.gameObject.SetActive(true);
            ammoIcon.gameObject.SetActive(true);
            weaponIcon.GetComponentInChildren<Image>().sprite = passedWeapon.gunSprite;
            weaponName.text = passedWeapon.name;
            weaponAmmo.text = "Ammo: " + passedWeapon.currentAmmoCount.ToString() + " / " + passedWeapon.maxAmmoCount;
        }
    }

    //This method will automatically run when player equip slot is empty.
    public void DisplayWeapon()
    {
        weaponIcon.GetComponentInChildren<Image>().sprite = null;
        weaponIcon.gameObject.SetActive(false);
        ammoIcon.gameObject.SetActive(false);
        weaponName.text = "";
        weaponAmmo.text = "";
    }

    public void DisplayHealth(HealthClass health)
    {
        numberHealth.text = health.GetHealth() + " / " + health.GetMaxHealth();
        playerHealthBar.value = health.GetHealth();
    }
}
