using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptble Object/Item/Generic Item", order = 2)]
public class SOItem : ScriptableObject
{
    public new string name;
    [TextArea(20, 20)]
    public string description = "Comment Here.";

    public GameObject prefab;
    public Image image;
}
