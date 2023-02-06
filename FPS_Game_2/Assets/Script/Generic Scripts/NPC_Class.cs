using UnityEngine;
using UnityEngine.UI;

public class NPC_Class : MonoBehaviour
{

    /// <summary>
    /// This class will reference the different scripts the NPC will have and handle
    /// all game operations relative to the NPC.
    /// </summary>
    /// 

    public HealthClass healthClassScript;



    //ScriptableObject data.
    public NPC_ScriptableObject npcSO;  //Any value from npcSO should only be used in start.

    public Slider slider;
    public Gradient gradient;   //Gradient to change color of slider depending on amount of health. (Green -> red)
    public Image fill;  //Color of slider.


    private bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        healthClassScript = GetComponent<HealthClass>();
        npcSO = GetComponent<NPC_ScriptableObject>();

        healthClassScript.SetMaxHealth(npcSO.health);
        healthClassScript.ResetHealth();

        //UI stuff.
        GameObject healthUI = Instantiate(Resources.Load("Prefabs/EnemyHealth"), transform) as GameObject;
        slider = healthUI.GetComponentInChildren<Slider>();
        slider.maxValue = healthClassScript.GetHealth();
        slider.value = slider.maxValue;
        fill = slider.transform.Find("Fill Area/Fill").GetComponentInChildren<Image>();
        fill.color = gradient.Evaluate(1f);
        slider.gameObject.SetActive(false);
        isDead = false;
    }

    /// <summary>
    /// If boolean for displayHealth in health is true, displays UI for health.
    /// </summary>
    public void DisplayHealth()
    {
        slider.gameObject.SetActive(true);
    }

    public void UnDisplayHealth()
    {
        slider.gameObject.SetActive(false);
    }

    /// <summary>
    /// Changes health by amount and also displays health depending on whether
    /// displayHealth bool is true.
    /// </summary>
    /// <param name="amount"></param>
    /// 
    public void HealthSlider()
    {
        slider.value = healthClassScript.GetHealth();
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
