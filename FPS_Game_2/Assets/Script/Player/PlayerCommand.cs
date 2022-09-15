using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCommand : MonoBehaviour
{
    #region PossibleBug
    /*
     * [Bug] Not sure if bug but for some reason, if [Serializefield] is not attributed to player,
     * game does not run. Making player public works but if private or internal, it must have 
     * serializefield or it doesn't work.
     * 
     * [SerializeField] Player player = Player.Instance;             (Works)
     * [SerializeField] private Player player = Player.Instance;     (Works)
     * [SerializeField] internal Player player = Player.Instance;    (Works)
     * public Player player = Player.Instance;                       (Works)
     * Player player = Player.Instance;                              (Does not work)
     * private Player player = Player.Instance;                      (Does not work)
     * internal Player player = Player.Instance;                     (Does not work)
     * 
     * Error- (NullReferenceException: Object reference not set to an instance of an object)
     * */
    #endregion

    [SerializeField] Player player = Player.Instance;   //local reference
    private int weaponPointer;

    void Start()
    {
        weaponPointer = 0;
    }

    /// <summary>
    /// Basic player movement function. Uses rigid MovePosition method.
    /// </summary>
    /// <param name="direction"></param>    Takes in the direction player is moving.
    /// <param name="speed"></param>        Player's speed.
    internal void MoveCharacter(Vector3 direction, float speed)
    {
        //Player.Instance.rigidBody.MovePosition(transform.position + (direction * speed * Time.deltaTime));
        player.rigidBody.MovePosition(transform.position + (direction * speed * Time.deltaTime));
    }

    /// <summary>
    /// Causes movement with gravity effecting movement. Uses rigidbody AddForce. Good for sliding, slipping, etc.
    /// </summary>
    /// <param name="direction"></param>    Takes in the direction player is moving.
    /// <param name="speed"></param>        Player's speed.
    internal void MoveCharacterForce(Vector3 direction, float speed)
    {
        player.rigidBody.AddForce(direction * speed);
    }

    /// <summary>
    /// Command for player jumping.
    /// </summary>
    /// <param name="jumpHeight"></param>
    internal void Jump(float jumpHeight)
    {
        player.rigidBody.velocity += (Vector3.up * jumpHeight);
    }

    /// <summary>
    /// Runs if the player presses mouse scroll wheel. Changes cursor state.
    /// </summary>
    /// <returns></returns>
    internal CursorLockMode ChangeCursor()
    {
        return (player.cursorState) ? CursorLockMode.None : CursorLockMode.Locked;
    }

    internal void PickUp()
    {

    }

    internal void Drop()
    {

    }

    internal void ScrollWeapon()
    {
        int previousSelectedWeapon = weaponPointer;
        //If mouse scrollwheel up, selected weapon should increase. And if it goes beyond childcount, drops to 0.
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            weaponPointer = (weaponPointer >= transform.childCount - 1) ? 0 : weaponPointer++;
        }
        //If mouse scrollwheel down, selected weapon should decrease. And if it goes below 0, it goes to total child count.
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            weaponPointer = (weaponPointer <= 0) ? weaponPointer = transform.childCount - 1 : weaponPointer--;
        }
    }

    internal void KeyWeapon()
    {

    }

    internal void SelectWeapon()
    {
        int i = 0;
        foreach (GameObject weapon in player.playerInventory.inventory)
        {
            if (i == weaponPointer)
            {
                weapon.gameObject.SetActive(true);
                Player.Instance.currentWeapon = weapon.gameObject;
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }

}
