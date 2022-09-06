using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerCommand))]   //Contains all functions for player.


public class Player : MonoBehaviour
{
    #region Singleton
    public static Player Instance { get; private set; }     //Singleton
    #endregion

    #region Components
    internal Rigidbody rigidBody;
    [SerializeField] internal static PlayerCommand playerCommand;
    #endregion

    #region Player Data
    [SerializeField] bool isGrounded;    //Checks to see if player is grounded. Checks with playermodel collider.
    internal float distanceToGround;

    [SerializeField] internal float moveSpeed;

    public Vector3 movementDirection;
    internal bool jump;
    public float jumpHeight;

    internal bool cursorState;  //0 = Locked 1 = NotLocked
    #endregion

    #region GameObjects
    [SerializeField] internal GameObject player;    //Main player gameobject.
    [SerializeField] internal GameObject playerModel;     //player gameobject model.
    [SerializeField] internal GameObject head;      //gameobject for player's head.
    #endregion


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
        Cursor.lockState = CursorLockMode.Locked;   //Makes cursor disappear when game starts.
        #region PlayerData
        moveSpeed = 10f;
        jumpHeight = 5f;
        distanceToGround = 0.5f;
        #endregion

        #region Reference
        rigidBody = GetComponent<Rigidbody>();          //RigidBody reference.
        rigidBody.freezeRotation = true;                //Prevents collisions from knocking player down.
        playerCommand = GetComponent<PlayerCommand>();  //PlayerCommand reference.
        player = GameObject.FindGameObjectWithTag("Player");
        playerModel = GameObject.FindGameObjectWithTag("PlayerModel");  //GameObject that contains this script.
        head = GameObject.FindGameObjectWithTag("Head");    //GameObject for the head.

        #endregion

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
        #endregion

        LinkPlayerToPlayerModel();
    }

    void Update()
    {
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
        //moveSpeed = (jump) ? (moveSpeed / 2) : (moveSpeed * 2); When player jumps, should reduce movement position.
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

        #region IsGrounded function
        //Checks the distance between playermodel and ground and returns true/false
        isGrounded = (Physics.Raycast(playerModel.transform.position, Vector3.down, distanceToGround + 0.1f)) ? true : false;
        #endregion

    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if ((int)collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if ((int)collision.gameObject.layer != LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }

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
