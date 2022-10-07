using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Will handle all inputs made by player.
/// </summary>
public class InputManager : MonoBehaviour
{
    #region Singleton
    private static InputManager _instance;
    public static InputManager Instance
    {
        get { return _instance; }
    }
    #endregion


    private PlayerControls playerControls;  //InputControls for player.

    private void Awake()
    {
        #region Setting Singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            _instance = this;
        }
        #endregion
        playerControls = new PlayerControls();
    }

    void Start()
    {
        if (playerControls == null)
        {
            Debug.LogError("No InputSystem class Playercontrols() created for PlayerInputManager.");
        }
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return playerControls.Player.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta()
    {
        return playerControls.Player.Look.ReadValue<Vector2>();
    }

    public bool PlayerJumped()
    {
        return playerControls.Player.Jump.triggered;
    }

    public float PlayerRun()
    {
        return playerControls.Player.Run.ReadValue<float>();
    }

    private void FixedUpdate()
    {
        

    }

    #region Commands


    internal void PickUp()
    {

    }

    internal void Drop()
    {

    }

    internal void ScrollWeapon()
    {

    }

    internal void KeyWeapon()
    {

    }

    //internal void SelectWeapon()
    //{
    //    int i = 0;
    //    foreach (GameObject weapon in player.playerInventory.inventory)
    //    {
    //        if (i == weaponPointer)
    //        {
    //            weapon.gameObject.SetActive(true);
    //            Player.Instance.currentWeapon = weapon.gameObject;
    //        }
    //        else
    //        {
    //            weapon.gameObject.SetActive(false);
    //        }
    //        i++;
    //    }
    //}

    #endregion

}
