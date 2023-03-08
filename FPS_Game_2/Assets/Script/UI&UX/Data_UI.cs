using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_UI : MonoBehaviour
{
    public GameObject startingReference;    //This is the starting point where it will get child from.
    public GameObject[] referenceData;

    private void Awake()
    {
        if (startingReference == null)
            Debug.LogError("Missing referenceData for gameObject starting referenceData.");
        referenceData = new GameObject[startingReference.transform.childCount];
        int i = 0;
        foreach (Transform child in startingReference.transform)
        {
            if (referenceData[i] == null)
            {
                referenceData[i] = child.gameObject;
            }
            i++;
        }
    }
    // Just in case there is an error.
    void Start()
    {
        int i = 0;
        foreach (GameObject child in referenceData)
        {
            if (child == null)
                Debug.LogError("Missing referenceData to gameobject for referenceData array at location " + i);
            i++;
        }
    }
}
