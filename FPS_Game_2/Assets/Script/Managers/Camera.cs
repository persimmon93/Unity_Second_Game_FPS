using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for rotation of camera angle.
/// NOTE: This script must be executed after the player script because it depends on references that are
/// initialized in the player script.
/// </summary>
public class Camera : MonoBehaviour
{
    #region Singleton
    public static Camera Instance { get; private set; }     //Singleton
    #endregion

    private void Awake()
    {
        #region SettingSingleton
        //Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        #endregion
    }

    public Transform target;

    [Range(100f, 300f)]
    public float mouseSensitivity;

    [SerializeField] internal float lookX;
    [SerializeField] internal float lookY;

    //Amount player is able to rotate axis.
    private float maxAngleRotationUpwards = -60f;
    private float maxAngleRotationDownwards = 60f;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (Player.Instance.player != null)
        {
            Debug.Log("Ran");
            CameraReference();
        }

        mouseSensitivity = 200f;

        GameObject head = Player.Instance.head;
        //Sets camera position and rotation to where the head is looking forward + 0.1 position.z. This is to prevent clipping.
        transform.SetPositionAndRotation(head.transform.position + new Vector3(0, 0, 0.15f), head.transform.localRotation);
        transform.parent = Player.Instance.transform;
    }

    private void Update()
    {
        #region Player Rotation with Mouse.
        lookX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        lookY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, maxAngleRotationUpwards, maxAngleRotationDownwards);  //

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        target.Rotate(Vector3.up * lookX);
        #endregion

    }
    // Update is called once per frame
    void LateUpdate()
    {

    }

    //References and exepction calls for Camera script.
    private void CameraReference()
    {
        //Sets reference to player.
        target = Player.Instance.player.transform;

        #region Exception Call
        //Exception Call
        if (Camera.Instance.target == null)
        {
            Debug.LogError("The camera is missing reference for target. There might be no tag 'Player' set on a game object.");
        }
        #endregion
    }
}
