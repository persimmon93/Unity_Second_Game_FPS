//using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class CameraClass : MonoBehaviour
{
    [HeaderAttribute("Camera Components")]

    public UI_MainHandler userInterface;
    private float clampAngle = 80f;

    [Range(100f, 300f)]
    public float mouseSensitivity = 200f;

    private Vector3 rotateCamera;

    [HeaderAttribute("Interaction")]
    [SerializeField] protected float farInteractionRange = 15f;
    [SerializeField] protected float closeInteractionRange = 0.7f;
    public GameObject waitingPickUp;

    private void Start()
    {

    }

    private void OnEnable()
    {
        if (userInterface == null)
            Debug.LogWarning("Userinterface reference missing for camera");
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
        FarInteraction();
        CloseInteraction();
    }

    private void CameraRotation()
    {
        rotateCamera.x += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        rotateCamera.y += Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        rotateCamera.y = Mathf.Clamp(rotateCamera.y, -clampAngle, clampAngle);

        //This will handle rotate camera.
        transform.rotation = Quaternion.Euler(-rotateCamera.y, rotateCamera.x, 0f);
    }

    private void FarInteraction()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.GetPoint(0f), transform.forward * farInteractionRange, Color.red);
        if (Physics.Raycast(ray, out RaycastHit hit, farInteractionRange) && hit.transform.gameObject.tag != "Untagged")
        {
            string gameObjectTag = (string)hit.transform.gameObject.tag;
            userInterface.FarInteraction(hit, gameObjectTag);   //Passes in object hit and the tag of hit object.
        } else
        {
            userInterface.FarInteraction();  //Makese Active = false.
        }
    }

    private void CloseInteraction()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.GetPoint(0f), transform.forward * closeInteractionRange, Color.green);
        if (Physics.Raycast(ray, out RaycastHit hit, closeInteractionRange) && hit.transform.gameObject.tag != "Untagged")
        {
            //Checks to see if item is able to be picked up. Gets info to be displayed before pick up.
            if (hit.transform.gameObject.GetComponent<ItemPickUp>())
            {
                waitingPickUp = hit.transform.gameObject;
            }
            string gameObjectTag = (string)hit.transform.gameObject.tag;
            userInterface.CloseInteraction(hit, gameObjectTag);
        } else
        {
            if (waitingPickUp != null)
            {
                waitingPickUp = null;
            }
            userInterface.CloseInteraction();
        }
    }
}