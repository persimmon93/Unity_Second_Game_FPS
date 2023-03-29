using FPS_Game;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainClass_NPC : MonoBehaviour
{

    /// <summary>
    /// This class will referenceData the different scripts the NPC will have and handle
    /// all game operations relative to the NPC.
    /// </summary>
    /// 

    //Personal Info
    private new string name;
    private string description;
    private float movementSpeed;
    private float fieldVision;
    [SerializeField] private LivingClass npcClassification;
    [SerializeField] private bool essentialCharacter;   //If true, prevents death.

    [SerializeField] internal HealthClass npcHealth;

    //ScriptableObject data.
    public CharacterScriptableObject scriptableObject;  //Any value from SOLivingObject should only be used in start.


    private bool isDead;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        
    }

    private void OnEnable()
    {
        if (scriptableObject == null)
        {
            Debug.LogError(this.gameObject + " is missing a referenceData to scriptable object.");
        }
        //gameObject.tag = "NPC";
        //Setting NPC Health
        npcHealth = (gameObject.GetComponent<HealthClass>() == null) ? gameObject.AddComponent<HealthClass>(): 
            gameObject.GetComponent<HealthClass>();
        npcHealth.SetMaxHealth(scriptableObject.health);
        npcHealth.ResetHealth();

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

    public void Death()
    {
        if (!essentialCharacter && npcHealth.GetHealth() <= 0f)
        {
            Destroy(gameObject);
        }
    }

    //This is an interface method that will call on the changehealth methon in
    //the healthclass script and change the health.
    public void Damage()
    {
        npcHealth.ChangeHealth(2);
    }
}
