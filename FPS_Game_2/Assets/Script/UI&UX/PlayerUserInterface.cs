using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class PlayerUserInterface : MonoBehaviour
{
    [SerializeField] internal Image cursor;

    //UI reference for target the player is focused on.
    public TMP_Text targetName;
    public Slider targetHealthSlider;
    public Gradient healthGradient;   //Gradient to change color of slider depending on amount of health. (Green -> red)
    public Image fill;  //Color of slider.


    //Public ItemClass item;
    public GameObject itemParent;
    public TMP_Text itemName;
    public TMP_Text itemDescription;
    public TMP_Text itemAmmo;

    public void SetTargetInfo(string name, float maxHealth, float currentHealth)
    {
        targetName.text = name;
        targetHealthSlider.maxValue = maxHealth;
        targetHealthSlider.value = currentHealth;
    }

    /// <summary>
    /// If boolean for displayHealth in health is true, displays UI for health.
    /// </summary>
    public void DisplayHealth(float maxHealth, float currentHealth)
    {
        targetName.gameObject.SetActive(true);
        targetHealthSlider.gameObject.SetActive(true);
        targetHealthSlider.maxValue = maxHealth;
        targetHealthSlider.value = currentHealth;
        fill.color = healthGradient.Evaluate(targetHealthSlider.normalizedValue);
    }

    public void UnDisplayHealth()
    {
        targetName.gameObject.SetActive(false);
        targetHealthSlider.gameObject.SetActive(false);
    }

    /// <summary>
    /// Changes health by amount and also displays health depending on whether
    /// displayHealth bool is true.
    /// </summary>
    /// <param name="amount"></param>
    /// 
    public void UpdateHealthSlider(float changeAmount)
    {
        targetHealthSlider.value += changeAmount;
        fill.color = healthGradient.Evaluate(targetHealthSlider.normalizedValue);
    }

    /// <summary>
    /// Takes in parameters for weapons.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="ammoCount"></param>
    /// <param name="maxAmmo"></param>
    public void DisplayItem(string name, string description, int ammoCount, int maxAmmo)
    {
        itemParent.gameObject.SetActive(true);
        itemName.text = name;
        itemDescription.text = description;
        itemAmmo.text = ammoCount + " / " + maxAmmo;
        itemAmmo.gameObject.SetActive(true);
    }
    
    /// <summary>
    /// Takes in parameters for item display.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>
    public void DisplayItem(string name, string description)
    {
        itemParent.gameObject.SetActive(true);
        itemName.text = name;
        itemDescription.text = description;
        itemAmmo.gameObject.SetActive(false);
    }
    public void UnDisplayItem()
    {
        itemParent.gameObject.SetActive(false);
        itemName.text = "";
        itemDescription.text = "";
        itemAmmo.text = "";
    }
}