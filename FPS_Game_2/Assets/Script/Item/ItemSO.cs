using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/ItemData")]
public class ItemSO : ScriptableObject
{
    public int itemName;
    public GameObject itemPrefab;
}
