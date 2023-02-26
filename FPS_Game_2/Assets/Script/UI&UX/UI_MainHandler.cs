using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;

public class UI_MainHandler : MonoBehaviour
{
    #region Singleton
    public static UI_MainHandler Instance { get; private set; }     //Singleton
    #endregion

    public UI_Target uiTarget;
    public UI_Item uiItem;

    [SerializeField] internal Image cursor;

    private void Awake()
{
    #region SettingSingleton
    //Singleton
    if (Instance != null && Instance != this)
    {
        Destroy(this);
    } else
    {
        Instance = this;
    }
    #endregion
}


    private void Update()
    {

    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {

    }

    //Passed in parameters indicates that a far target is hit.
    //Activates the UI gameobject.
    public void FarInteraction(RaycastHit target, string objectTag)
    {
        switch(objectTag)
        {
            case "NPC":
                uiTarget.npc = target.transform.gameObject.GetComponent<MainClass_NPC>();
                uiTarget.transform.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
    //Deactivates the UI gameobject.
    public void FarInteraction()
    {
        uiTarget.transform.gameObject.SetActive(false);
    }

    //Passed in parameters indicates that a close target is hit.
    //Activates the UI gameobject.
    public void CloseInteraction(RaycastHit target, string objectTag)
    {
        switch (objectTag)
        {
            case "Item":
                uiItem.weapon = target.transform.gameObject.GetComponent<WeaponClass>();
                uiItem.transform.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
    //Deactivates the UI gameobject.
    public void CloseInteraction()
    {
        uiItem.transform.gameObject.SetActive(false);
    }
}