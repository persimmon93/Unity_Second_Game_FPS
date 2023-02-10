using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HealthClass), typeof(NPC_Class))]
public class NPC_Class : MonoBehaviour
{

    /// <summary>
    /// This class will reference the different scripts the NPC will have and handle
    /// all game operations relative to the NPC.
    /// </summary>
    /// 

    public HealthClass healthClassScript;
    public Rigidbody rb;    //Maybe set private later
    public CapsuleCollider cc;  //maybe set private later

    //ScriptableObject data.
    public NPC_ScriptableObject NPC_SO;  //Any value from NPC_SO should only be used in start.

    public Slider slider;
    public Gradient gradient;   //Gradient to change color of slider depending on amount of health. (Green -> red)
    public Image fill;  //Color of slider.


    private bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        //Setting NPC Health
        healthClassScript = GetComponent<HealthClass>();
        healthClassScript.SetMaxHealth(NPC_SO.health);
        healthClassScript.ResetHealth();

        //RigidBody
        rb = transform.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionY;
        rb.isKinematic = true;
        rb.useGravity = true;

        //Capsule Collider
        cc = transform.AddComponent<CapsuleCollider>();
        cc.isTrigger = true;
        cc.center = new Vector3(0, 1, 0);
        cc.radius = 0.3f;
        cc.height = 2;

        //Create game object model if it doesn't exist.
        CreateModel();

        //UI stuff.
        GameObject healthUI = Instantiate(Resources.Load("Prefabs/EnemyHealth"), transform) as GameObject;
        healthUI.transform.localPosition = new Vector3(0, 1.5f, 0);
        healthUI.transform.localScale = new Vector3(0.5f, 0.5f, 1);

        slider = healthUI.GetComponentInChildren<Slider>();
        slider.maxValue = healthClassScript.GetHealth();
        slider.value = slider.maxValue;
        fill = slider.transform.Find("Fill Area/Fill").GetComponentInChildren<Image>();
        fill.color = gradient.Evaluate(1f); //Not Working
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

    public void CreateModel()
    {
        //Instantiate game object model if it is null.
        foreach (GameObject gameObjectModel in transform)
        {
            if (gameObjectModel.name == "Model")
            {
                return;
            }
        }
        Instantiate(NPC_SO.npc_gameobject, transform);
    }

    //Physics for NPC
    private void PullGameObjectDown()
    {
        //if (rb.useGravity && cc.)
    }
}
