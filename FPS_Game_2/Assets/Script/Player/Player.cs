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
    [Range(10f, 100f)]
    public float mouseSensitivity = 70f;


    [SerializeField] bool isGrounded;    //Checks to see if player is grounded.
    public float distanceToGround = 1f;

    [SerializeField] internal float moveSpeed;
    public Vector3 movementDirection;
    internal bool jump;
    [SerializeField] public float jumpHeight;

    #endregion


    #region GameObjects
    internal GameObject player;     //GameObject that contains this script.
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
        #region PlayerData
        moveSpeed = 10f;
        jumpHeight = 10f;

        #endregion

        #region Reference
        rigidBody = GetComponent<Rigidbody>();          //RigidBody reference.
        rigidBody.freezeRotation = true;                //Prevents collisions from knocking player down.
        playerCommand = GetComponent<PlayerCommand>();  //PlayerCommand reference.
        player = transform.gameObject;                  //GameObject that contains this script.
        #endregion

        #region ExceptionCalls
        if (playerCommand == null)
        {
            Debug.LogError("Player script is missing 'PlayerCommand' component!");
        }
        if (player == null)
        {
            Debug.LogError("GameObject for player is missing!");
        }
        #endregion
    }

    void Update()
    {
        movementDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (isGrounded && Input.GetButtonDown("Jump"))
            jump = true;

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
        //Checks the distance between player and ground and returns true/false
        isGrounded = (Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.1f)) ? true : false;
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

}
