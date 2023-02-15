using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(HealthClass))]


public class MainClass_Player : MonoBehaviour
{
    #region Singleton
    //public static Player Instance { get; private set; }     //Singleton
    #endregion

    [HeaderAttribute("ScriptableObject")]
    public SOLivingObject scriptableObject;

    [HeaderAttribute("Player Model")]
    [SerializeField] public GameObject playerModel;
    [SerializeField] public GameObject player;

    [HeaderAttribute("Health")]
    public HealthClass healthClassScript;

    [HeaderAttribute("PlayerController")]
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [Range(2f, 10f)]
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    private InputManager inputManager;
    private Transform cameraTransform;

    bool isDead;

    /*private void Awake()
    {
        #region SettingSingleton
        //Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        } else
        {
            Instance = this;
        }
        #endregion
    }
    */

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        inputManager = InputManager.Instance;
    }

    private void OnEnable()
    {
        if (scriptableObject == null)
        {
            Debug.LogError("Player is missing reference to Scriptable Object");
        }
        healthClassScript = GetComponent<HealthClass>();
        healthClassScript.SetMaxHealth(scriptableObject.health);
        healthClassScript.ResetHealth();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = -0.1f;
        }
        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0f, movement.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0f;        //move.y may move after above line so sets it to 0 so that player can always jump.
        controller.Move(move * Time.deltaTime * playerSpeed);

        // Changes the height position of the player.
        if (inputManager.PlayerJumped() && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        //Makes transform rotate in the direction of camera.
        transform.rotation = Quaternion.Euler(0, cameraTransform.rotation.eulerAngles.y, 0);
        //cameraTransform.localEulerAngles = new Vector3(move.x, 0, 0);

        //Debug.Log(inputManager.PlayerRun());
    }

    private void LateUpdate()
    {

    }
}
