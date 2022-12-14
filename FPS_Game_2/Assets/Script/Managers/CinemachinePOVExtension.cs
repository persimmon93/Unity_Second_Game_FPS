using UnityEngine;
using Cinemachine;

//Overrides the CinemachineVirtual camaera in player so that angle in Aim can be changed by InputManager's mouse delta.
public class CinemachinePOVExtension : CinemachineExtension
{
    [SerializeField]
    private float mouseSpeed = 10f;

    [SerializeField]
    private float clampAngle = 80f;


    private InputManager inputManager;
    private Vector3 startingRotation;
    protected override void Awake()
    {
        inputManager = InputManager.Instance;
        if (startingRotation == null)
        {
            startingRotation = transform.localRotation.eulerAngles;
        }
        base.Awake();
    }
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                Vector2 deltaInput = inputManager.GetMouseDelta();
                startingRotation.x += deltaInput.x * mouseSpeed * Time.deltaTime;
                startingRotation.y += deltaInput.y * mouseSpeed * Time.deltaTime;
                //x rotation should not be clamped or it will prevent player from turning around.
                //startingRotation.x = Mathf.Clamp(startingRotation.x, -clampAngle, clampAngle);
                startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);
                state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);
            }
        }
    }
}
