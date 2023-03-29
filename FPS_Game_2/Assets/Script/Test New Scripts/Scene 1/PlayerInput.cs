using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS_Game
{
    /// <summary>
    /// This class will detect player's input and correspond it to an action in game.
    /// Gets a reference to the player class for data 
    /// </summary>
    public class PlayerInput : MonoBehaviour
    {

        private Player player;

        [HeaderAttribute("Player Input")]
        [SerializeField] private bool isMoving;
        [SerializeField] private bool isJumping;
        [SerializeField] private bool isRunning;
        [SerializeField] private bool isFire1;
        [SerializeField] private bool isFire2;
        [SerializeField] internal bool isPickingUp;
        [SerializeField] private bool isReloading;
        [SerializeField] private bool isDroppingItem;
        bool isDead;


        private void OnEnable()
        {
            player = GetComponent<Player>();
        }

        void Update()
        {
            //userInterface.PlayerInfoSetWeapon(itemEquip);

            InputFire1();
            InputFire2();
            //Below actions shoud not run if not grounded.
            if (player.isGrounded == false)
                return;

            InputMovement();
            InputJump();
            InputRunning();
            InputPickUp();
            InputReload();


            if (Input.GetKeyDown(KeyCode.T))
            {
                player.health.ChangeHealth(-10);
                //player.userInterface.PlayerInfoQuickChangeHealth(mainClass.playerHealth);
            }
        }

        private void LateUpdate()
        {
            player.controller.Move(player.velocity * Time.deltaTime);
        }

        private void InputMovement()
        {
            isMoving = (Input.GetButton("Horizontal") || Input.GetButton("Vertical")) ? true : false;
            if (isMoving)
                //Moves toward direction of camera.
                player.velocity = (transform.right * Input.GetAxis("Horizontal") +
                    transform.forward * Input.GetAxis("Vertical"));

            player.velocity = (isMoving) ?
                Vector3.ClampMagnitude(player.velocity, 1f) * player.speed
                : player.velocity = Vector3.ClampMagnitude(player.velocity, 0);

            //Run Animator here.
        }

        public void InputJump()
        {
            isJumping = (Input.GetButton("Jump")) ? true : false;
            player.velocity.y = (isJumping) ?
                Mathf.Sqrt(player.jumpHeight * -2f * player.gravityValue)
                : 0f;

            //Run Animator here.
        }

        private void InputRunning()
        {
            isRunning = (Input.GetKey(KeyCode.LeftShift) ? true : false);
            player.speed += (isRunning) ? player.speed * 0.1f : -player.speed * 0.1f;
            player.speed = Mathf.Clamp(player.speed, 2f, 6f);

        }

        //Change this to get itemEquip from inventory
        private void InputFire1()
        {
            isFire1 = (Input.GetButton("Fire1")) ? true : false;
            //if (isFire1 && player.itemEquip != null)
            //{
            //    mainClass.itemEquip.GetComponent<GunClass>().PrimaryUse();
            //    mainClass.userInterface.PlayerInfoSetWeapon(mainClass.itemEquip);
            //}
        }

        private void InputFire2()
        {
            isFire2 = (Input.GetButton("Fire2")) ? true : false;
        }

        private void InputReload()
        {
            //isReloading = (Input.GetKey(KeyCode.R)) ? true : false;
            //if (isReloading && mainClass.itemEquip != null)
            //{
            //    WeaponType equippedWeaponType = mainClass.itemEquip.GetComponent<WeaponType>();
            //    int ammoInInventory = mainClass.inventory.GetAmmoCount(equippedWeaponType);

            //    mainClass.inventory.ChangeAmmoValue(equippedWeaponType,
            //        mainClass.itemEquip.GetComponent<GunClass>().Reload(mainClass.inventory.GetAmmoCount(equippedWeaponType)));
            //    //inventory.ammo = itemEquip.GetComponent<GunClass>().Reload(inventory.ammo);
            //    //inventory.GetAmmoCount(itemEquip.GetComponent<WeaponType>());
            //    mainClass.userInterface.PlayerInfoSetWeapon(mainClass.itemEquip);
            //}
        }

        private void InputPickUp()
        {
            //isPickingUp = (Input.GetKey(KeyCode.E)) ? true : false;
            //if (isPickingUp && !mainClass.itemEquipped)
            //{
            //    if (mainClass.itemEquip == null)
            //        return;
            //    mainClass.itemEquip.transform.parent = mainClass.itemHoldPosition.transform;
            //    mainClass.itemEquip.transform.localPosition = Vector3.zero;
            //    mainClass.itemEquip.transform.localRotation = Quaternion.Euler(Vector3.zero);
            //    transform.localScale = Vector3.one;
            //    //equippedWeapon.transform.forward = itemHoldPosition.transform.forward;
            //    mainClass.itemEquipped = true;

            //    mainClass.userInterface.PlayerInfoSetWeapon(mainClass.itemEquip);
            //}
        }

        private void InputDropItem()
        {
            //isDroppingItem = (Input.GetKey(KeyCode.G)) ? true : false;
            //if (isDroppingItem && mainClass.itemEquipped)
            //{
            //    mainClass.itemEquip = null;
            //    return;

            //}
        }
    }
}