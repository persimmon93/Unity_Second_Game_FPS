//using Cinemachine;
using FPS_Game;
using UnityEngine;
using UserInterface;


namespace Player
{
    /// <summary>
    /// Handles camera rotation and function.
    /// </summary>
    public class PlayerCamera : MonoBehaviour
    {
        private PlayerMainClass mainClass;

        [HeaderAttribute("Camera Components")]
        private Manager_UI userInterface;
        private float clampAngle = 80f;

        [Range(100f, 300f)]
        public float mouseSensitivity = 200f;

        private Vector3 rotateCamera;

        [HeaderAttribute("Interaction")]
        [SerializeField] protected float maxInteractionDistance = 15f;
        [SerializeField] protected float closeInteractionDistance = 0.7f;

        private void Start()
        {

        }

        private void OnEnable()
        {
            mainClass = transform.GetComponentInParent<PlayerMainClass>();
            userInterface = mainClass.userInterface;
            if (userInterface == null)
                Debug.LogWarning("Userinterface referenceData doesn't exist for parent of camera");
            if (rotateCamera == null)
                rotateCamera = transform.localRotation.eulerAngles;
        }

        // Update is called once per frame
        void Update()
        {
            CameraRotation();
        }

        private void FixedUpdate()
        {
            Interaction();
        }

        private void CameraRotation()
        {
            rotateCamera.x += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            rotateCamera.y += Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            rotateCamera.y = Mathf.Clamp(rotateCamera.y, -clampAngle, clampAngle);

            //This will handle rotate camera.
            transform.rotation = Quaternion.Euler(-rotateCamera.y, rotateCamera.x, 0f);
        }

        /// <summary>
        /// 
        /// </summary>
        private void Interaction()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.GetPoint(0f), transform.forward * maxInteractionDistance, Color.red);
            if (Physics.Raycast(ray, out RaycastHit hit, maxInteractionDistance))
            {
                IPickUpAble pickUpAble = hit.transform.GetComponent<IPickUpAble>();
                if (pickUpAble != null && mainClass.controls.isPickingUp)
                {
                    //Returns gameobject prefab of interactable
                    mainClass.itemEquip = hit.transform.GetComponent<IPickUpAble>().PickUp();
                }
                userInterface.TargetItem(hit);
                userInterface.TargetNPC(hit);   //Passes in object hit.
            }
        }
    }
}