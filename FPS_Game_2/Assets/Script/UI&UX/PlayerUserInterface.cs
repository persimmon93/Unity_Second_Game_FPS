using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUserInterface : MonoBehaviour
{
    //UI reference for target the player is focused on.
    public MainClass_NPC target;
    public TMP_Text targetName;
    public Slider targetHealthSlider;
    public Gradient healthGradient;   //Gradient to change color of slider depending on amount of health. (Green -> red)
    public Image fill;  //Color of slider.

    public void SetTargetInfo(string name, float maxHealth, float currentHealth)
    {
        targetName.text = name;
        targetHealthSlider.maxValue = maxHealth;
        targetHealthSlider.value = currentHealth;
    }

    /// <summary>
    /// If boolean for displayHealth in health is true, displays UI for health.
    /// </summary>
    public void DisplayHealth()
    {
        targetName.gameObject.SetActive(true);
        targetHealthSlider.gameObject.SetActive(true);
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
    public void HealthSlider()
    {
        targetHealthSlider.maxValue = target.healthClassScript.GetMaxHealth();
        targetHealthSlider.value = target.healthClassScript.GetHealth();
        fill.color = healthGradient.Evaluate(targetHealthSlider.normalizedValue);
    }
}