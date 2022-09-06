using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        if (Player.Instance.player != null)
        {
            camera.transform.position = Player.Instance.transform.position;
            //camera.transform.
            camera.transform.SetParent(Player.Instance.player.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
