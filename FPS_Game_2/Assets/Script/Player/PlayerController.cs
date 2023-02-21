using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    private void Start()    //Maybe make onEnabled
    {
        controller = (controller == null) ? gameObject.AddComponent<CharacterController>()
            : GetComponent<CharacterController>();

    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    /// <summary>
    /// This method receives the input command from the player for horizontal and vertical movement.
    /// </summary>
    /// <param name="playerMoveVector"></param>
    /// <param name="speed"></param>
    public void PlayerMove(Vector3 playerMoveVector, float speed)
    {
        if (!groundedPlayer)
            return;
        controller.Move(playerMoveVector * Time.deltaTime * speed);
        if (playerMoveVector != Vector3.zero)
        {
            gameObject.transform.forward = playerMoveVector;    //Makes gameobject face forward to movement.
        }
    }

    public void Jump(Vector3 playerJumpVector, float jumpHeight)
    {
        if (!groundedPlayer)
            return;
        playerJumpVector.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
