using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UITarget : MonoBehaviour
{
    //UI reference for target the player is focused on.
    public TMP_Text targetName;
    public Slider targetHealthSlider;
    public Gradient healthGradient;   //Gradient to change color of slider depending on amount of health. (Green -> red)
    public Image fill;  //Color of slider.

    public MainClass_NPC npc;   //If ray detects npc, it should set it as soon as set.

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
        targetName.gameObject.SetActive(true);
        targetHealthSlider.gameObject.SetActive(true);

        if (npc != null)
        {
            targetName.text = npc.name;
            targetHealthSlider.maxValue = npc.healthClassScript.GetMaxHealth();
            targetHealthSlider.value = npc.healthClassScript.GetHealth();
            fill.color = healthGradient.Evaluate(targetHealthSlider.normalizedValue);
        }
    }

    public void DeactivateUI()
    {
        targetName.text = "";
        targetHealthSlider.maxValue = 0;
        targetHealthSlider.value = 0;

        npc = null;

        targetName.gameObject.SetActive(false);
        targetHealthSlider.gameObject.SetActive(false);
    }


    /// <summary>
    /// Displays changed health on UI.
    /// </summary>
    /// <param name="amount"></param>
    /// 
    public void ChangeHealth(float changeAmount)
    {
        targetHealthSlider.value += changeAmount;
        fill.color = healthGradient.Evaluate(targetHealthSlider.normalizedValue);
    }
}
