using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Class that will handle player movement, jump, and checks to see if player is grounded.
public class PlayerMotor : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 movementDirection;
    private Vector3 velocity;

    [Range(5f, 20f)]
    [SerializeField] internal float jumpHeight, moveSpeed;
    private float distanceToGround; //Distance in which isGrounded will be considered true.
    public bool isGrounded;    //Checks to see if player is touching ground.

    private bool run, jump;

    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector3.zero;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;                //Prevents collisions from knocking player down.
        moveSpeed = 10f;
        jumpHeight = 5f;
        distanceToGround = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        movementDirection = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        //Checks the distance between playermodel and ground and returns true/false
        isGrounded = (Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.01f)) ? true : false;
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + (movementDirection * moveSpeed * Time.fixedDeltaTime));
        if (jump)
        {
            rb.velocity += (Vector3.up * jumpHeight);
            jump = false;
        }
    }
}
