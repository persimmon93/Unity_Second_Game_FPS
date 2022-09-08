using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerCommand))]   //Contains all functions for player.
[RequireComponent(typeof(Target))]          //Class to identify gameobject as attackable and able to take damage.
                                            //This contains requirecomponent for rigidbody.
//[RequireComponent(typeof(Rigidbody))]     Is in Target script.
[RequireComponent(typeof(PlayerItem))]      //Script that will handle all items for player.

/// NOTE: This script must be executed before the Camera script because the Camera script
/// depends on references that are initialized in this script.
[DefaultExecutionOrder(0)] //This should execute this script before any other script but if an error occurs,
                           //then adjust so this script runs before Camera script in Execution Order.

public class Player : MonoBehaviour
{
    #region Singleton
    public static Player Instance { get; private set; }     //Singleton
    #endregion

    #region Components
    internal Rigidbody rigidBody;
    internal PlayerCommand playerCommand;
    internal PlayerItem playerItem;
    #endregion

    #region Player Data
    [SerializeField] bool isGrounded;    //Checks to see if player is grounded. Checks with playermodel collider.
    internal float distanceToGround;

    [Range(5f, 20f)]
    [SerializeField] internal float moveSpeed;

    public Vector3 movementDirection;
    internal bool jump;
    internal bool fire;         //Bool for firing gun. Use for sound and awareness of npcs from gun shot sound.
    public float jumpHeight;

    internal bool cursorState;  //0 = Locked 1 = NotLocked
    #endregion

    #region UI
    internal Image crossHair;   //Set Manually.
    #endregion

    #region GameObjects
    [SerializeField] internal GameObject player;    //Main player gameobject.
    [SerializeField] internal GameObject playerModel;     //player gameobject model.
    [SerializeField] internal GameObject head;      //gameobject for player's head.
    #endregion

    public GameObject currentWeapon;

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
        PlayerReference();
        LinkPlayerToPlayerModel();

        Cursor.lockState = CursorLockMode.Locked;   //Makes cursor disappear when game starts.
        #region PlayerData
        moveSpeed = 10f;
        jumpHeight = 5f;
        distanceToGround = 0.5f;
        #endregion
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            fire = true;
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            currentWeapon.transform.GetComponent<Weapon>().Reload();
        }

        movementDirection = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");

        #region MouseCursor
        if (Input.GetMouseButtonDown(2))
        {
            cursorState = !cursorState;
            Cursor.lockState = playerCommand.ChangeCursor();
        }
        #endregion

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        #region Movement
        playerCommand.MoveCharacter(movementDirection, moveSpeed);
        #endregion

        #region Jump
        if (jump)
        {
            playerCommand.Jump(jumpHeight);
            jump = false;
        }
        #endregion

        #region Using Weapon
        if (fire)
        {
            currentWeapon.transform.GetComponent<Weapon>().Attack(Camera.Instance.transform);
            fire = false;
        }
        #endregion

        #region IsGrounded function
        //Checks the distance between playermodel and ground and returns true/false
        isGrounded = (Physics.Raycast(playerModel.transform.position, Vector3.down, distanceToGround + 0.1f)) ? true : false;
        #endregion

    }


    //References and exception calls for Player script. Will run once and throw exceptions to null references.
    private void PlayerReference()
    {
        #region Reference
        rigidBody = transform.GetComponent<Rigidbody>();          //RigidBody reference.
        rigidBody.freezeRotation = true;                //Prevents collisions from knocking player down.
        playerCommand = transform.GetComponent<PlayerCommand>();  //PlayerCommand script reference.
        player = GameObject.FindGameObjectWithTag("Player");    //GameObject for player parent. (Empty object).
        playerModel = GameObject.FindGameObjectWithTag("PlayerModel");  //GameObject referencing the player model.
        head = GameObject.FindGameObjectWithTag("Head");    //GameObject referencing for the player's head.
        playerItem = transform.GetComponent<PlayerItem>();  //Script that will contian all relative data for items.
        #endregion

        //Stops game if references are null.
        #region ExceptionCalls
        if (playerCommand == null)
        {
            Debug.LogError("Player script is missing 'PlayerCommand' component!");
        }
        if (player == null)
        {
            Debug.LogError("Reference for Player is missing! Set a tag 'Player' for a game object.");
        }
        if (playerModel == null)
        {
            Debug.LogError("Reference for PlayerModel is missing! Set a tag 'PlayerModel' for a game object.");
        }
        if (head == null)
        {
            Debug.LogError("Reference for Head is missing! Set a tag 'Head' for a game object.");
        }
        if (playerItem == null)
        {
            Debug.LogError("Player script is missing 'PlayerItem' component!");
        }
        #endregion

        //Will notify player if these references are null.
        #region NotificationCalls
        if (crossHair == null)
        {
            Debug.Log("There is no crosshair for player.");
        }
        #endregion
    }


    /// <summary>
    /// This will attach player model to the player object via script without manually attaching model to player.
    /// </summary>
    private void LinkPlayerToPlayerModel()
    {
        if (playerModel.transform.parent == null)
        {
            playerModel.transform.SetParent(player.transform);
        }
        //Sets the position of player model to equal position of player.
        playerModel.transform.position = player.transform.position;
        if (playerModel.GetComponent<CapsuleCollider>() == null)
        {
            playerModel.AddComponent<CapsuleCollider>();
            //If the y-axis for Vector3 is 1, it sets isGrounded to always be false. Anywhere between 0.96-0.99 is ideal.
            playerModel.GetComponent<CapsuleCollider>().center = new Vector3(0, 0.97f, 0);
            playerModel.GetComponent<CapsuleCollider>().height = 2;
        }
    }
}
