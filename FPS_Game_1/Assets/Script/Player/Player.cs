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


    [SerializeField] internal static Rigidbody rigidBody;
    [SerializeField] bool isGrounded;    //Checks to see if player is grounded.

    [SerializeField] internal static PlayerCommand playerCommand;

    public float speed;
    public Vector3 movementDirection;

    private void Awake()
    {
        //Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        } else
        {
            Instance = this;
        }
    }

    void Start()
    {
        #region PlayerData
        speed = 10f;


        #endregion

        #region Reference
        rigidBody = GetComponent<Rigidbody>();          //RigidBody reference.
        rigidBody.freezeRotation = true;                //Prevents collisions from knocking player down.
        playerCommand = GetComponent<PlayerCommand>();  //PlayerCommand reference.
        #endregion

        #region ExceptionCalls
        if (playerCommand == null)
        {
            Debug.LogError("Player script is missing 'PlayerCommand' component!");
        }
        #endregion
    }

    void Update()
    {
        movementDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        playerCommand.MoveCharacter(movementDirection, speed);
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
