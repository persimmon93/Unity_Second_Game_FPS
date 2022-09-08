using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//REMOVED SCRIPT FROM GAME. MOVED FUNCTIONS BACK TO SCRIPTS RELATIVE TO REFERENCE CALLS.


/// <summary>
/// This script will handle all references and throw an exception if a reference turns up null.
/// This should be set to run first in the script execution order in the player settings.
/// If there is a script with ExecutionOrder less than 0, change the order so this script runs first.
/// If this script does not run before other scripts, it may cause an error due to other scripts
/// having references to null while trying to activate something.
/// </summary>
/// 
[DefaultExecutionOrder(0)]
public class ReferenceManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerReference();
        CameraReference();
    }

    //References and exception calls for Player script.
    private void PlayerReference()
    {
        #region Reference
        Player.Instance.rigidBody = Player.Instance.GetComponent<Rigidbody>();          //RigidBody reference.
        Player.Instance.rigidBody.freezeRotation = true;                //Prevents collisions from knocking player down.
        Player.Instance.playerCommand = Player.Instance.GetComponent<PlayerCommand>();  //PlayerCommand script reference.
        Player.Instance.player = GameObject.FindGameObjectWithTag("Player");    //GameObject for player parent. (Empty object).
        Player.Instance.playerModel = GameObject.FindGameObjectWithTag("PlayerModel");  //GameObject referencing the player model.
        Player.Instance.head = GameObject.FindGameObjectWithTag("Head");    //GameObject referencing for the player's head.
        #endregion

        #region ExceptionCalls
        if (Player.Instance.playerCommand == null)
        {
            Debug.LogError("Player script is missing 'PlayerCommand' component!");
        }
        if (Player.Instance.player == null)
        {
            Debug.LogError("Reference for Player is missing! Set a tag 'Player' for a game object.");
        }
        if (Player.Instance.playerModel == null)
        {
            Debug.LogError("Reference for PlayerModel is missing! Set a tag 'PlayerModel' for a game object.");
        }
        if (Player.Instance.head == null)
        {
            Debug.LogError("Reference for Head is missing! Set a tag 'Head' for a game object.");
        }
        #endregion
    }

    //References and exepction calls for Camera script.
    private void CameraReference()
    {
        //Sets reference to player.
        Camera.Instance.target = Player.Instance.player.transform;

        #region Exception Call
        //Exception Call
        if (Camera.Instance.target == null)
        {
            Debug.LogError("The camera is missing reference for target. There might be no tag 'Player' set on a game object.");
        }
        #endregion
    }
}
