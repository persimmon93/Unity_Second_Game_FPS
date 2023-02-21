using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualInputManager : MonoBehaviour
{
    #region Singleton
    private static ManualInputManager _instance;
    public static ManualInputManager Instance
    {
        get { return _instance; }
    }
    #endregion

    private void Awake()
    {
        #region Setting Singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        #endregion
    }

    public void PlayerMovement(Vector3 InputAxis, float speed)
    {
        
    }


}
