using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Living Object", menuName = "Scriptble Object/Living Object", order = 1)]

public class NPCScriptableObject : ScriptableObject
{
    public new string name;
    [TextArea(20, 20)]
    public string description = "Comment Here.";

    public GameObject prefab;
    public Image image;

    //The max range of this NPC's health.
    [Range(100f, 100000f)]
    public float health;
    [Range(1f, 10f)]
    public float speed;
    [Range(50f, 500f)]
    public float field_vision;

    [SerializeField] internal LivingClass classification;


    //Effect displayed upon being hit.
    public GameObject hitEffect;
}

public enum LivingClass
{
    Player,
    Enemy,
    Friendly
}