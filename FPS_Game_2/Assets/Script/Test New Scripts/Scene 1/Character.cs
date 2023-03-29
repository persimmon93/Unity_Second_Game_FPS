using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace FPS_Game
{
    [RequireComponent(typeof(Health), typeof(CharacterController))]
    /// <summary>
    /// This is an abstract class that will be inherited by
    /// the player and all npcs. All settings for both player
    /// and npc should be written here.
    /// </summary>
    public abstract class Character : MonoBehaviour
    { 
        [TextArea(5, 3)]
        public string description = "Description of Character";

        [HeaderAttribute("ScriptableObject")]
        public CharacterScriptableObject characterScriptableObject;

        //This is the gameobject that contains the mesh and colliders.
        public GameObject model;
        internal Health health;

        //This is a default value of health that will be
        //stored in the class health which handles all health
        //related activities.
        [Range(100f, 100000f)]
        private float defaultHealth = 100f;
        [Range(1f, 10f)]
        [SerializeField] internal float speed = 2f;
        [SerializeField] internal float jumpHeight = 1.0f;
        [Range(50f, 500f)]
        [SerializeField] internal float field_vision = 100f;

        [HeaderAttribute("Controller and Physics")]
        internal CharacterController controller;
        internal float gravityValue = -9.81f;
        internal Vector3 velocity = new Vector3(0, 0, 0);
        internal float maxDistance = 0.1f;   //Distance for isGrounded to be true.
        internal LayerMask checkGroundLayer;
        internal bool isGrounded;



        internal virtual void OnEnable()
        {
            controller = GetComponent<CharacterController>();
            checkGroundLayer = LayerMask.GetMask("Ground");
            GrabModel();
            //If the character has a scriptableObject, it should
            //replace the stats with the stats provided from scriptable object.
            if (characterScriptableObject != null)
            {
                CharacterScriptableObject so = characterScriptableObject;
                transform.name = so.name;
                description = so.description;
                model = so.prefab;
                defaultHealth = so.health;
                speed = so.speed;
                field_vision = so.field_vision;
            }

            if (health == null)
            {
                health = GetComponent<Health>();
            }
            health.SetMaxHealth(defaultHealth);
            health.ResetHealth();
        }

        internal virtual void Update()
        {
            IsGroundedImplementation();
        }

        internal virtual void LateUpdate()
        {

        }

        #region Getting Model
        /// <summary>
        /// This method will attempt to get a reference for the game object containing the mesh
        /// of the character.
        /// 
        /// This is under the assumption that the first child is the player model.
        /// </summary>
        private void GrabModel()
        {
            if (transform.childCount == 0)
            {
                Debug.LogWarning(transform + " does not contain a child game object pertaining to game model. Creating new empty game object.");
                model = new GameObject("model");
                model.transform.parent = transform;
                model.transform.localScale = Vector3.one;
                model.transform.localPosition = Vector3.zero;
                model.transform.localRotation = Quaternion.identity;
            } else
            {
                //This will attempt to grab gameObject containing model in character.
                if (model == null)
                {
                    model = transform.GetChild(0).gameObject;
                    //Checking to see if first child is named model. If not, will send warning
                    //and change name of child to model.
                    if (model.name != name)
                    {
                        model.name = "model";
                        Debug.LogWarning(transform + "did not contain name 'model' check if model is correctly referenced.");
                    }
                }
            }
        }
        #endregion

        #region Physics for gravity
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
                velocity.y += gravityValue * Time.deltaTime;
        }

        #endregion
    }
}
