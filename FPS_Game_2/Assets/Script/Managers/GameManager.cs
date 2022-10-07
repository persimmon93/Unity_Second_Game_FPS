using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
    }
    #endregion

    private void Awake()
    {
        #region SettingSingleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            _instance = this;
        }
        #endregion
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


}
