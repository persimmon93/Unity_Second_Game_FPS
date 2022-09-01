using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCommand : MonoBehaviour
{
    /// <summary>
    /// Basic player movement function. Uses rigid MovePosition method.
    /// </summary>
    /// <param name="direction"></param>    Takes in the direction player is moving.
    /// <param name="speed"></param>        Player's speed.
    internal void MoveCharacter(Vector3 direction, float speed)
    {
        Player.rigidBody.MovePosition(transform.position + (direction * speed * Time.deltaTime));
    }

    /// <summary>
    /// Causes movement with gravity effecting movement. Uses rigidbody AddForce. Good for sliding, slipping, etc.
    /// </summary>
    /// <param name="direction"></param>    Takes in the direction player is moving.
    /// <param name="speed"></param>        Player's speed.
    internal void MoveCharacterForce(Vector3 direction, float speed)
    {
        Player.rigidBody.AddForce(direction * speed);
    }
    
    /// <summary>
    /// 
    /// </summary>
    internal void Jump(float speed)
    {

    }
}
