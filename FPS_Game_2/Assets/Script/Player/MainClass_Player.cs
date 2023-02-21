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

    [HeaderAttribute("Inventory")]
    public InventoryClass inventory;

    [HeaderAttribute("PlayerController")]
    [SerializeField] private CharacterController controller;
    [Range(2f, 6f)]
    [SerializeField] private float playerSpeed;
    [SerializeField] private float acceleration = 0.1f; //Handles acceleration when running.
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private Vector3 playerVelocity = new Vector3(0, 0, 0);
    float maxDistance = 0.1f;   //Distance for isGrounded to be true.
    [SerializeField] private LayerMask checkGroundLayer;

    [SerializeField] private float rangeToPickUpItems = 3f;
    private ManualInputManager inputManager;
    private Camera camera;

    [HeaderAttribute("Player Status")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isMoving;
    [SerializeField] private bool isJumping;
    [SerializeField] private bool isRunning;
    [SerializeField] private bool isFire1;
    [SerializeField] private bool isFire2;
    [SerializeField] private bool canPickUp;
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

    }

    private void OnEnable()
    {
        if (scriptableObject == null)
        {
            Debug.LogError("Player is missing reference to Scriptable Object");
        }

        controller = GetComponent<CharacterController>();
        isGrounded = controller.isGrounded;
        camera = Camera.main;
        inputManager = ManualInputManager.Instance;

        healthClassScript = (healthClassScript == null) ? gameObject.AddComponent<HealthClass>()
            : gameObject.GetComponent<HealthClass>();
        healthClassScript.SetMaxHealth(scriptableObject.health);
        healthClassScript.ResetHealth();

        inventory = (inventory == null) ? gameObject.AddComponent<InventoryClass>() :
            gameObject.GetComponent<InventoryClass>();
    }

    void Update()
    {
        //Makes transform rotate in the direction of camera.
        transform.rotation = Quaternion.Euler(0, camera.transform.rotation.eulerAngles.y, 0);
        IsGroundedImplementation();
        //Below actions shoud not run if not grounded.
        if (!isGrounded)
            return;

        InputMovement();
        InputJump();
        InputRunning();
        InputFire1();
        InputFire2();
        PickUpWeapon();
    }

    private void LateUpdate()
    {
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void InputMovement()
    {
        isMoving = (Input.GetButton("Horizontal") || Input.GetButton("Vertical")) ? true : false;
        if (isMoving)
            //Moves toward direction of camera.
            playerVelocity = (transform.right * Input.GetAxis("Horizontal") +
                transform.forward * Input.GetAxis("Vertical"));

        playerVelocity = (isMoving) ? Vector3.ClampMagnitude(playerVelocity, 1f) * playerSpeed :
            playerVelocity = Vector3.ClampMagnitude(playerVelocity, 0);

        //Run Animator here.
    }

    public void InputJump()
    {
        isJumping = (Input.GetButton("Jump")) ? true : false;
        playerVelocity.y = (isJumping) ? Mathf.Sqrt(jumpHeight * -2f * gravityValue) : 0f;

        //Run Animator here.
    }

    private void InputRunning()
    {
        isRunning = (Input.GetKey(KeyCode.LeftShift) ? true : false);
        playerSpeed += (isRunning) ? acceleration : -acceleration;
        playerSpeed = Mathf.Clamp(playerSpeed, 2f, 6f);

    }

    private void InputFire1()
    {
        isFire1 = (Input.GetButton("Fire1")) ? true : false;

    }

    private void InputFire2()
    {
        isFire2 = (Input.GetButton("Fire2")) ? true : false;

    }

    private void PickUpWeapon()
    {
        RaycastHit raycastHit;
        canPickUp = (Physics.Raycast(Camera.main.transform.position,
            Camera.main.transform.forward,
            out raycastHit,
            rangeToPickUpItems)
            == gameObject.GetComponent<WeaponClass>()) ? true : false;
        //if (canPickUp)
    }
    //Maybe make this a manager method, accessible by all things.
    private void IsGroundedImplementation()
    {
        RaycastHit raycastHit;
        isGrounded = Physics.BoxCast(controller.bounds.center, transform.localScale, Vector3.down, out raycastHit,
            transform.rotation, maxDistance, checkGroundLayer);
        //Debugging
        Color rayColor;
        rayColor = (raycastHit.collider != null) ? Color.green : Color.red;
        //Boundary of controller
        Debug.DrawRay(controller.bounds.center + new Vector3(controller.bounds.extents.x, 0), Vector3.down * (controller.bounds.extents.y + maxDistance), rayColor);
        Debug.DrawRay(controller.bounds.center - new Vector3(controller.bounds.extents.x, 0), Vector3.down * (controller.bounds.extents.y + maxDistance), rayColor);
        Debug.DrawRay(controller.bounds.center - new Vector3(controller.bounds.extents.x, controller.bounds.extents.y), Vector3.right * (controller.bounds.extents.y), rayColor);
        GravityImplementation();
    }

    private void GravityImplementation()
    {
        if (!isGrounded)
            playerVelocity.y += gravityValue * Time.deltaTime;
    }
    private void ForAutoInputManager()
    {
        //Vector2 movement = inputManager.GetPlayerMovement();
        //Vector3 move = new Vector3(movement.x, 0f, movement.y);
        //move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        //move.y = 0f;        //move.y may move after above line so sets it to 0 so that player can always jump.
        //controller.Move(move * Time.deltaTime * playerSpeed);

        //// Changes the height position of the player.
        //if (inputManager.PlayerJumped() && groundedPlayer)
        //{
        //    playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);

        //playerVelocity.y += gravityValue * Time.deltaTime;
        //controller.Move(playerVelocity * Time.deltaTime);
        //}
    }
}
