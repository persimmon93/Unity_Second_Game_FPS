using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera playerCam;

    [Range(100f, 300f)]
    public float mouseSensitivity = 200f;

    [SerializeField] private float lookX, lookY;

    //Amount player is able to rotate axis.
    private float maxAngleRotationUpwards = -60f;
    private float maxAngleRotationDownwards = 60f;
    float xRotation = 0f;

    private void Awake()
    {
        if (playerCam == null)
        {
            playerCam = transform.GetComponentInChildren<CinemachineVirtualCamera>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (playerCam == null)
        {
            Debug.LogError("There is no component for cinemachine camera within player.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CameraRotation2();
    }

    //private void CameraRotation()
    //{
    //    lookX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
    //    lookY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

    //    xRotation -= lookY;
    //    xRotation = Mathf.Clamp(xRotation, maxAngleRotationUpwards, maxAngleRotationDownwards);

    //    //camera.transform.localRotation = Quaternion.AngleAxis(xRotation * mouseSensitivity, Vector3.up);
    //    playerCam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    //    transform.Rotate(Vector3.up * lookX);
    //}

    private void CameraRotation2()
    {
        transform.rotation = Quaternion.AngleAxis(lookX * mouseSensitivity, Vector3.up);

        var angles = transform.localEulerAngles;
        angles.z = 0;

        var angle = transform.localEulerAngles.x;
        if (angle > 180 && angle < 340)
        {
            angles.x = 340;
        } else if (angle < 180 && angle > 40)
        {
            angles.x = 40;
        }

        transform.localEulerAngles = angles;
    }
}