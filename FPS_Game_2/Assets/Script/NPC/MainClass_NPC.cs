using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainClass_NPC : MonoBehaviour
{

    /// <summary>
    /// This class will reference the different scripts the NPC will have and handle
    /// all game operations relative to the NPC.
    /// </summary>
    /// 

    //Personal Info
    private string name;
    private string description;
    private float movementSpeed;
    private float fieldVision;
    [SerializeField] private LivingClass npcClassification;
    [SerializeField] private bool essentialCharacter;   //If true, prevents death.

    [SerializeField] internal HealthClass healthClassScript;

    //ScriptableObject data.
    public NPCScriptableObject scriptableObject;  //Any value from SOLivingObject should only be used in start.


    private bool isDead;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        if (scriptableObject == null)
        {
            Debug.LogError(this.gameObject + " is missing a reference to scriptable object.");
        }
        //gameObject.tag = "NPC";
        //Setting NPC Health
        healthClassScript = (gameObject.GetComponent<HealthClass>() == null) ? gameObject.AddComponent<HealthClass>(): 
            gameObject.GetComponent<HealthClass>();
        healthClassScript.SetMaxHealth(scriptableObject.health);
        healthClassScript.ResetHealth();

        //Setting Info
        name = scriptableObject.name;
        description = scriptableObject.description;
        movementSpeed = scriptableObject.speed;
        fieldVision = scriptableObject.field_vision;

        gameObject.tag = "NPC";
        npcClassification = scriptableObject.classification;
        //Setting hit effect. Instantiated object when it is hit with bullet.
        //Create game object model if it doesn't exist.
    }

    //Physics for NPC
    private void PullGameObjectDown()
    {
        //if (rb.useGravity && cc.)
    }

    public string GetName()
    {
        return name;
    }

    public string GetDescription()
    {
        return description;
    }
}
