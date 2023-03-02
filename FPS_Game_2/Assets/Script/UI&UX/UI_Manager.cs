using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;

public class UI_Manager : MonoBehaviour
{
    #region Singleton
    public static UI_Manager Instance { get; private set; }     //Singleton
    #endregion

    public UI_Target uiTarget;
    public UI_Item uiItem;
    public UI_PlayerInfo uiPlayerInfo;

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


    if (uiTarget == null)
        uiTarget = GetComponentInChildren<UI_Target>();
    if (uiItem == null)
        uiItem = GetComponentInChildren<UI_Item>();
    if (uiPlayerInfo == null)
        uiPlayerInfo = GetComponentInChildren<UI_PlayerInfo>();
    }


    private void Start()
    {
        if (uiTarget == null)
            Debug.LogError("UI_Manager script is missing reference to UI_Target.");
        if (uiItem == null)
            Debug.LogError("UI_Manager script is missing reference to UI_Item.");
        if (uiPlayerInfo == null)
            Debug.LogError("UI_Manager script is missing reference to UI_PlayerInfo.");
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
                uiItem.weapon = target.transform.gameObject.GetComponent<GunClass>();
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



    //PlayerClass will pass in gunclass if weapon is equipped.
    public void UpdatePlayerUIWeapon(GunClass equippedWeapon)
    {
        if (equippedWeapon == null)
            uiPlayerInfo.DisplayWeapon();
        uiPlayerInfo.DisplayWeapon(equippedWeapon);
    }
    //Deactivates the UI gameobject.
    public void UpdatePlayerUIHealth(HealthClass health)
    {
        uiPlayerInfo.DisplayHealth(health);
    }
}