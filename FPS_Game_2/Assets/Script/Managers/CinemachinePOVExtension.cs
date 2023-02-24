//using UnityEngine;
//using Cinemachine;
//using Unity.VisualScripting;

////Overrides the CinemachineVirtual camaera in player so that angle in Aim can be changed by InputManager's mouse delta.
//public class CinemachinePOVExtension : CinemachineExtension
//{

//    public PlayerUserInterface userInterface;
//    [SerializeField]
//    private float mouseSpeed = 10f;

//    [SerializeField]
//    private float clampAngle = 80f;

//    float rangeToPickUpItems = 3f;

//    RaycastHit hit;

//    private InputManager inputManager;
//    private Vector3 startingRotation;
//    protected override void Awake()
//    {
//        inputManager = InputManager.Instance;
//        if (startingRotation == null)
//        {
//            startingRotation = transform.localRotation.eulerAngles;
//        }
//        base.Awake();
//    }
//    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
//    {
//        if (vcam.Follow)
//        {
//            if (stage == CinemachineCore.Stage.Aim)
//            {
//                Vector2 deltaInput = inputManager.GetMouseDelta();
//                startingRotation.x += deltaInput.x * mouseSpeed * Time.deltaTime;
//                startingRotation.y += deltaInput.y * mouseSpeed * Time.deltaTime;
//                //x rotation should not be clamped or it will prevent player from turning around.
//                //startingRotation.x = Mathf.Clamp(startingRotation.x, -clampAngle, clampAngle);
//                startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);
//                state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);
//            }
//        }
//    }

//    private void OnEnable()
//    {
//        if (userInterface != null)
//            Debug.LogWarning("Userinterface reference missing for camera");
//    }
//    private void FixedUpdate()
//    {
//        //DisplayEnemy();
//        DisplayWeapon();
//    }

//    private void DisplayEnemy()
//    {
//        if (Physics.Raycast(transform.position, transform.forward, out hit, 200f))  //Change 200f too field vision of scriptable object.
//        {
//            //Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); 
//            Debug.DrawRay(transform.position, transform.forward * 100.0f, Color.red);
//            //var target = hit.transform.GetComponent<MainClass_NPC>();
//            MainClass_NPC target = hit.transform.GetComponent<MainClass_NPC>();

//            //If target is null, it is an environment.
//            if (target != null)
//            {
//                //userInterface.DisplayHealth(target.healthClassScript.GetMaxHealth(), target.healthClassScript.GetHealth());
//                //userInterface.SetTargetInfo(target.name, target.healthClassScript.GetMaxHealth(), target.healthClassScript.GetHealth());
//            }
//            else
//            {
//                //userInterface.UnDisplayHealth();
//            }
//        }
//    }

//    private void DisplayWeapon()
//    {
//        if (Physics.Raycast(transform.position, transform.forward, out hit, rangeToPickUpItems))
//        {
//            Debug.DrawRay(transform.position, transform.forward * 100.0f, Color.yellow);
//            WeaponClass weapon = hit.transform.GetComponent<WeaponClass>();
//            if (weapon != null)
//            {
//                userInterface.DisplayItem(weapon.GetName(), weapon.GetDescription(), weapon.GetAmmoCount(), weapon.GetMaxAmmo());
//            } else
//            {
//                userInterface.UnDisplayItem();
//            }
//        }
//    }


//}
