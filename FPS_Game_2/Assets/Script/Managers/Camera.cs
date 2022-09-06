using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;

    [Range(100f, 300f)]
    public float mouseSensitivity;

    [SerializeField] internal float lookX;
    [SerializeField] internal float lookY;
    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        mouseSensitivity = 200f;

        //Sets reference to player.
        target = Player.Instance.player.transform;
        //Sets reference to head. (Which is where the camera position and rotation will be.
        GameObject head = Player.Instance.head;

        #region Exception Call
        //Exception Call
        if (target == null)
        {
            Debug.LogError("The camera is missing reference for target. Set tag 'Player' on a game object.");
        }
        if (head == null)
        {
            Debug.LogError("The camera is missing reference for head. Set tag 'Head' on a game object.");
        }
        #endregion

        //Sets camera position and rotation to where the head is looking forward.
        transform.SetPositionAndRotation(head.transform.position, head.transform.localRotation);
        transform.parent = head.transform;
    }

    private void Update()
    {
        #region Player Rotation with Mouse.
        lookX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        lookY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        target.Rotate(Vector3.up * lookX);
        #endregion

    }
    // Update is called once per frame
    void LateUpdate()
    {

    }
}
