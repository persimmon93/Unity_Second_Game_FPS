//using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] public Camera camera;

    public PlayerUserInterface userInterface;
    private float clampAngle = 80f;
    private float rangeToPickUpItems = 3f;
    [Range(100f, 300f)]
    public float mouseSensitivity = 200f;

    private Vector3 rotateCamera;

    private void OnEnable()
    {
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
        DisplayEnemy();
        DisplayWeapon();
    }

    private void FixedUpdate()
    {

    }

    private void CameraRotation()
    {
        rotateCamera.x += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        rotateCamera.y += Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        rotateCamera.y = Mathf.Clamp(rotateCamera.y, -clampAngle, clampAngle);

        //This will handle rotate camera.
        camera.transform.rotation = Quaternion.Euler(-rotateCamera.y, rotateCamera.x, 0f);
    }

    private void DisplayEnemy()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.GetPoint(0f), transform.forward * 100.0f, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, 200f))  //Change 200f too field vision of scriptable object.
        {
            MainClass_NPC target = hit.transform.GetComponent<MainClass_NPC>();

            //If target is null, it is an environment.
            if (target != null)
            {
                userInterface.DisplayHealth(target.healthClassScript.GetMaxHealth(), target.healthClassScript.GetHealth());
                userInterface.SetTargetInfo(target.name, target.healthClassScript.GetMaxHealth(), target.healthClassScript.GetHealth());
            }
            else
            {
                userInterface.UnDisplayHealth();
            }
        }
    }

    private void DisplayWeapon()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, rangeToPickUpItems))
        {
            WeaponClass weapon = hit.transform.GetComponent<WeaponClass>();
            if (weapon != null)
            {
                userInterface.DisplayItem(weapon.GetName(), weapon.GetDescription(), weapon.GetAmmoCount(), weapon.GetMaxAmmo());
            }
            else
            {
                userInterface.UnDisplayItem();
            }
        }
    }
}