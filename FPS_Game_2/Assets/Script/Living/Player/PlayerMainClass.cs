using UnityEngine;
using UserInterface;


namespace Player
{
    [RequireComponent(typeof(CharacterController))]

    public class PlayerMainClass : MonoBehaviour
    {
        #region Singleton
        //public static Player Instance { get; private set; }     //Singleton
        #endregion


        [HeaderAttribute("ScriptableObject")]
        public NPCScriptableObject scriptableObject;    //Will serve as the data storage for player.

        [HeaderAttribute("Health")]
        internal HealthClass playerHealth;

        [HeaderAttribute("Camera")]
        internal PlayerCamera cameraClass;

        [HeaderAttribute("Inventory")]
        internal InventoryClass inventory;
        public GameObject itemHoldPosition;
        public GameObject itemEquip;    //Will be Item/GunClass or any item equippable by player.
        internal bool itemEquipped;

        [HeaderAttribute("UserInterface")]
        public Manager_UI userInterface;

        [HeaderAttribute("PlayerController")]
        internal CharacterController controller;
        public PlayerControls controls;
        internal float jumpHeight = 1.0f;
        internal float gravityValue = -9.81f;
        internal Vector3 playerVelocity = new Vector3(0, 0, 0);
        internal float maxDistance = 0.1f;   //Distance for isGrounded to be true.
        internal LayerMask checkGroundLayer;
        internal float rangeToPickUpItems = 3f;
        internal bool isGrounded;

        //userInterface.PlayerInfoSetWeapon(itemEquip); Should be placed after all changes so that ui updates.


        /*private void Awake()
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
        */
        private void Awake()
        {

        }

        private void Start()
        {
            if (scriptableObject == null) Debug.LogError("Missing Scriptable Object for player.");
            if (userInterface == null) Debug.LogError("Missing referenceData to MainUserInterface class.");
        }

        private void OnEnable()
        {
            if (scriptableObject == null) Debug.LogError("Player is missing referenceData to Scriptable Object");
            controller = GetComponent<CharacterController>();
            

            //Setting HealthClass.
            playerHealth = (gameObject.GetComponent<HealthClass>() == null) ?
                gameObject.AddComponent<HealthClass>() : gameObject.GetComponent<HealthClass>();
            playerHealth.SetMaxHealth(scriptableObject.health);
            playerHealth.ResetHealth();
            
            //Setting InventoryClass.
            inventory = (gameObject.GetComponent<InventoryClass>() == null) ?
                gameObject.AddComponent<InventoryClass>() : gameObject.GetComponent<InventoryClass>();

            //Setting CameraClass.
            cameraClass = Camera.main.GetComponent<PlayerCamera>();

            //Setting Controls
            controls = (gameObject.GetComponent<PlayerControls>() == null) ?
                gameObject.AddComponent<PlayerControls>() : gameObject.GetComponent<PlayerControls>();
            checkGroundLayer = LayerMask.GetMask("Ground");

            //Initialize player's health for UI.
            userInterface.PlayerInfoSetHealth(playerHealth);
        }

        void Update()
        {
            IsGroundedImplementation();

            //userInterface.PlayerInfoSetWeapon(itemEquip);
            //Makes transform rotate in the direction of camera.
            transform.rotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);
        }

        private void LateUpdate()
        {

        }

        //Maybe make this a manager method, accessible by all things.
        private void IsGroundedImplementation()
        {
            RaycastHit raycastHit;
            isGrounded = Physics.BoxCast(controller.bounds.center, transform.localScale, Vector3.down, out raycastHit,
                transform.rotation, maxDistance, checkGroundLayer);
            //Debugging
            Color rayColor;
            rayColor = (raycastHit.collider != null) ? Color.green : Color.red;
            //Boundary of controller
            Debug.DrawRay(controller.bounds.center + new Vector3(controller.bounds.extents.x, 0), Vector3.down * (controller.bounds.extents.y + maxDistance), rayColor);
            Debug.DrawRay(controller.bounds.center - new Vector3(controller.bounds.extents.x, 0), Vector3.down * (controller.bounds.extents.y + maxDistance), rayColor);
            Debug.DrawRay(controller.bounds.center - new Vector3(controller.bounds.extents.x, controller.bounds.extents.y), Vector3.right * (controller.bounds.extents.y), rayColor);
            GravityImplementation();
        }

        private void GravityImplementation()
        {
            if (!isGrounded)
                playerVelocity.y += gravityValue * Time.deltaTime;
        }
    }
}