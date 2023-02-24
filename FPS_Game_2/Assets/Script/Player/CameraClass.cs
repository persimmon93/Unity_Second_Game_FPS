//using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class CameraClass : MonoBehaviour
{
    [HeaderAttribute("Camera Components")]
    [SerializeField] public Camera camera;

    public UIMainHandler userInterface;
    private float clampAngle = 80f;

    [Range(100f, 300f)]
    public float mouseSensitivity = 200f;

    private Vector3 rotateCamera;

    [HeaderAttribute("Interaction")]
    [SerializeField] protected float farInteractionRange = 20f;
    [SerializeField] protected float closeInteractionRange = 3f;

    private void Start()
    {

    }

    private void OnEnable()
    {
        if (camera == null)
            camera = Camera.main;
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
        camera.transform.rotation = Quaternion.Euler(-rotateCamera.y, rotateCamera.x, 0f);
    }

    private void FarInteraction()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.GetPoint(0f), transform.forward * farInteractionRange, Color.red);
        if (Physics.Raycast(ray, out RaycastHit hit, farInteractionRange))
        {
            string gameObjectTag = (string)hit.transform.gameObject.tag;
            Debug.DrawRay(ray.GetPoint(0f), transform.forward * farInteractionRange, Color.green);
            userInterface.FarInteraction(hit, gameObjectTag);   //Makes Active = true.
        } else
        {
            userInterface.FarInteraction();  //Makese Active = false.
        }
    }

    private void CloseInteraction()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.GetPoint(0f), transform.forward * closeInteractionRange, Color.cyan);
        if (Physics.Raycast(ray, out RaycastHit hit, closeInteractionRange))
        {
            string gameObjectTag = (string)hit.transform.gameObject.tag;
            userInterface.CloseInteraction(hit, gameObjectTag);
        } else
        {
            userInterface.CloseInteraction();
        }
    }
}