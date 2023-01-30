using System.Data;
using Unity.VisualScripting;
using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86;

public class HealthNPC : HealthBase
{
    /// <summary>
    /// This class should be added to game objects that will possess health and 
    /// </summary>
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    [Range(0f, 1000f)]
    [SerializeField]
    private float npcHealth;

    private bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        if (npcHealth <= 0f)
        {
            npcHealth = SetInitialHealth();
        }
        GameObject healthUI = Instantiate(Resources.Load("Prefabs/EnemyHealth"), transform) as GameObject;
        slider = healthUI.GetComponentInChildren<Slider>();
        slider.maxValue = npcHealth;
        slider.value = slider.maxValue;
        fill = slider.transform.Find("Fill Area/Fill").GetComponentInChildren<Image>();
        fill.color = gradient.Evaluate(1f);
        slider.gameObject.SetActive(false);
        isDead = false;
    }

    /// <summary>
    /// If boolean for displayHealth in health is true, displays UI for health.
    /// </summary>
    public override void DisplayHealth()
    {
        slider.gameObject.SetActive(true);
    }

    /// <summary>
    /// Changes health by amount and also displays health depending on whether
    /// displayHealth bool is true.
    /// </summary>
    /// <param name="amount"></param>
    public override void ChangeHealth(float amount)
    {
        npcHealth += amount;
        if (npcHealth > slider.maxValue)
        {
            npcHealth = slider.maxValue;
        }
        slider.value = npcHealth;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        if (!isDead && slider.gameObject.activeSelf == false)
        {
            DisplayHealth();
        }
        //Checks to see if NPC is dead.
        Death();
    }

    //Made flexible for revival of npc.
    public override void Death()
    {
        isDead = (npcHealth <= 0) ? true : false;
        if (isDead)
        {
            npcHealth = 0;
            slider.gameObject.SetActive(false);
        }
    }
}
