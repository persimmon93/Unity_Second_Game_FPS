using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]


public class Player : MonoBehaviour
{
    #region Singleton
    //public static Player Instance { get; private set; }     //Singleton
    #endregion


    [HeaderAttribute("Player Model")]
    [SerializeField] internal GameObject playerModel;
    [SerializeField] internal GameObject player;

    [HeaderAttribute("Health")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth = 100f;
    public float GetHealth { get { return currentHealth; }}

    [HeaderAttribute("PlayerController")]
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [Range(2f, 10f)]
    private float playerSpeed = 211.0f;
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
        maxHealth = currentHealth;
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

        //Debug.Log(inputManager.PlayerRun());
    }

    private void LateUpdate()
    {

    }

    /// <summary>
    /// Method that will add or subtract amount passed in from health. Changes player's alive status after
    /// adding or subtracting health. This will be used by games to set 
    /// </summary>
    /// <param name="amount"></param> Amount to be added or subtracted from player's health.
    public void AddHealth(float amount)
    {
        float healthChange = currentHealth + amount;
        if (healthChange > maxHealth)
        {
            isDead = false;
            currentHealth = maxHealth;
        } else if (healthChange < 0){
            currentHealth = 0;
            isDead = true;
        }
        else
        {
            isDead = false;
            currentHealth = healthChange;
        }
    }

    //Enables player to set health regardless of game bound rules.
    public float SetHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }
}
