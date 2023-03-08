//using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class CameraClass : MonoBehaviour
{
    [HeaderAttribute("Camera Components")]

    private Manager_UI userInterface;
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
        userInterface = GetComponentInParent<MainClass_Player>().userInterface;
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

    /// <summary>
    /// 
    /// </summary>
    private void FarInteraction()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.GetPoint(0f), transform.forward * farInteractionRange, Color.red);
        if (Physics.Raycast(ray, out RaycastHit hit, farInteractionRange))
        {
            userInterface.TargetNPC(hit);   //Passes in object hit.
        }
    }

    private void CloseInteraction()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.GetPoint(0f), transform.forward * closeInteractionRange, Color.green);
        if (Physics.Raycast(ray, out RaycastHit hit, closeInteractionRange))
        {
            //Checks to see if item is able to be picked up. Gets info to be displayed before pick up.
            //Learn about event listener so that when button is pressed, picks up.
            if (hit.transform.gameObject.GetComponent<ItemPickUp>())
            {
                waitingPickUp = hit.transform.gameObject;
            }

            //Test this later Maybe
            if (GetComponentInParent<MainClass_Player>().transform.gameObject.GetComponent<ItemPickUp>())
            {
                waitingPickUp = hit.transform.gameObject;
            }
            userInterface.TargetItem(hit);
        }
    }
}