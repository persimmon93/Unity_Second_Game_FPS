using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]


public class Player : MonoBehaviour
{
    #region Singleton
    public static Player Instance { get; private set; }     //Singleton
    #endregion


    [HeaderAttribute("Player Model")]
    [SerializeField] internal GameObject playerModel;
    [SerializeField] internal GameObject player;

    [HeaderAttribute("Health")]
    public float maxHealth;
    [SerializeField] private float health = 100f;
    public float Health
    { 
        get { return health; }
    }

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


    private void Awake()
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

    void Start()
    {
        maxHealth = health;
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        inputManager = InputManager.Instance;
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

        HealthRange();
        //Debug.Log(inputManager.PlayerRun());
    }

    private void LateUpdate()
    {

    }

    //Prevents player health from decreasing below 0 or above maxHealth.
    private void HealthRange()
    {
        if (health < 0)
        {
            health = 0;
        }
        else if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    /// <summary>
    /// This will attach player model to the player object via script without manually attaching model to player.
    /// </summary>
    //private void LinkPlayerToPlayerModel()
    //{
    //    //Sets the position of player model to equal position of player.
    //    playerModel.transform.position = player.transform.position;
    //    if (playerModel.GetComponent<CapsuleCollider>() == null)
    //    {
    //        playerModel.AddComponent<CapsuleCollider>();
    //        //If the y-axis for Vector3 is 1, it sets isGrounded to always be false. Anywhere between 0.96-0.99 is ideal.
    //        playerModel.GetComponent<CapsuleCollider>().center = new Vector3(0, 0.97f, 0);
    //        playerModel.GetComponent<CapsuleCollider>().height = 2;
    //    }
    //}
}
