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

    public HealthClass healthClassScript;
    public Rigidbody rb;    //Maybe set private later
    public CapsuleCollider cc;  //maybe set private later and change to gameobject.

    //ScriptableObject data.
    public SOLivingObject scriptableObject;  //Any value from SOLivingObject should only be used in start.


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

        //Setting NPC Health
        healthClassScript = (healthClassScript == null) ? gameObject.AddComponent<HealthClass>() :
            GetComponent<HealthClass>();
        healthClassScript.SetMaxHealth(scriptableObject.health);
        healthClassScript.ResetHealth();

        //RigidBody
        rb = (rb == null) ? gameObject.AddComponent<Rigidbody>() :
            GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionY;
        rb.isKinematic = true;
        rb.useGravity = true;

        //Capsule Collider Later Change too game object.
        cc = (cc == null) ? gameObject.AddComponent<CapsuleCollider>() :
            GetComponent<CapsuleCollider>();
        cc.isTrigger = true;
        cc.center = new Vector3(0, 1, 0);
        cc.radius = 0.3f;
        cc.height = 2;

        //Setting Info
        name = scriptableObject.name;
        description = scriptableObject.description;
        movementSpeed = scriptableObject.speed;
        fieldVision = scriptableObject.field_vision;


        //Create game object model if it doesn't exist.
        CreateModel();
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
        Instantiate(scriptableObject.prefab, transform, true);
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
