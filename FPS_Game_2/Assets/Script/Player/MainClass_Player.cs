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
    public NPCScriptableObject so_player;

    [HeaderAttribute("Health")]
    protected HealthClass healthClassScript;

    [HeaderAttribute("Camera")]
    protected CameraClass cameraClass;
    public AudioSource audioSource;

    [HeaderAttribute("Inventory")]
    public InventoryClass inventory;
    public GameObject itemHoldPosition;
    public GunClass equippedWeapon;

    [HeaderAttribute("UserInterface")]
    public Manager_UI userInterface;


    [HeaderAttribute("PlayerController")]
    [SerializeField] private CharacterController controller;
    [Range(2f, 6f)]
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private Vector3 playerVelocity = new Vector3(0, 0, 0);
    float maxDistance = 0.1f;   //Distance for isGrounded to be true.
    [SerializeField] private LayerMask checkGroundLayer;

    [SerializeField] private float rangeToPickUpItems = 3f;

    [HeaderAttribute("Player Status")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isMoving;
    [SerializeField] private bool isJumping;
    [SerializeField] private bool isRunning;
    [SerializeField] private bool isFire1;
    [SerializeField] private bool isFire2;
    [SerializeField] internal bool isPickingUp;
    [SerializeField] private bool isReloading;
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
    private void Awake()
    {

    }

    private void Start()
    {
        if (so_player == null)
            Debug.LogError("Missing Scriptable Object for player.");
        if (userInterface == null)
            Debug.LogError("Missing referenceData to MainUserInterface class.");
    }

    private void OnEnable()
    {
        if (so_player == null)
        {
            Debug.LogError("Player is missing referenceData to Scriptable Object");
        }

        controller = GetComponent<CharacterController>();
        isGrounded = controller.isGrounded;

        healthClassScript = (gameObject.GetComponent<HealthClass>() == null) ? gameObject.AddComponent<HealthClass>()
            : gameObject.GetComponent<HealthClass>();
        healthClassScript.SetMaxHealth(so_player.health);
        healthClassScript.ResetHealth();

        inventory = (gameObject.GetComponent<InventoryClass>() == null) ? gameObject.AddComponent<InventoryClass>() :
            gameObject.GetComponent<InventoryClass>();

        cameraClass = Camera.main.GetComponent<CameraClass>();
        audioSource = cameraClass.GetComponent<AudioSource>();

        userInterface.PlayerInfoSetHealth(healthClassScript);
    }

    void Update()
    {
        //Makes transform rotate in the direction of camera.
        transform.rotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);
        IsGroundedImplementation();

        userInterface.PlayerInfoSetWeapon(equippedWeapon);

        InputFire1();
        InputFire2();
        //Below actions shoud not run if not grounded.
        if (!isGrounded)
            return;

        InputMovement();
        InputJump();
        InputRunning();
        InputPickUp();
        InputReload();


        if (Input.GetKeyDown(KeyCode.T))
        {
            healthClassScript.ChangeHealth(-10);
            userInterface.PlayerInfoQuickChangeHealth(healthClassScript);
        }
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

        playerVelocity = (isMoving) ? Vector3.ClampMagnitude(playerVelocity, 1f) * so_player.speed :
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
        so_player.speed += (isRunning) ? so_player.speed * 0.1f : -so_player.speed * 0.1f;
        so_player.speed = Mathf.Clamp(so_player.speed, 2f, 6f);

    }

    private void InputFire1()
    {
        isFire1 = (Input.GetButton("Fire1")) ? true : false;
        if (isFire1 && equippedWeapon != null)
        {
            equippedWeapon.Shoot();
        }
    }

    private void InputFire2()
    {
        isFire2 = (Input.GetButton("Fire2")) ? true : false;
    }

    private void InputReload()
    {
        isReloading = (Input.GetKey(KeyCode.R)) ? true : false;
        if (isReloading)
        {
            inventory.ammo = equippedWeapon.Reload(inventory.ammo);
        }
    }

    private void InputPickUp()
    {
        if (cameraClass.waitingPickUp == null)
            return;
        isPickingUp = (Input.GetKey(KeyCode.E)) ? true : false;
        if (isPickingUp)
        {
            equippedWeapon = cameraClass.waitingPickUp.GetComponent<GunClass>();
            //equippedWeapon.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            cameraClass.waitingPickUp.transform.parent = itemHoldPosition.transform;
            cameraClass.waitingPickUp.transform.localPosition = Vector3.zero;
            cameraClass.waitingPickUp.transform.localRotation = Quaternion.identity;

            userInterface.PlayerInfoSetWeapon(equippedWeapon);
        }
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
