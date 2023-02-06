using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC", menuName = "NPC", order = 1)]

[RequireComponent(typeof(HealthClass), typeof(NPC_Class))]
public class NPC_ScriptableObject : ScriptableObject
{
    public new string name;
    public static int id;
    [TextArea(100, 10000)]
    public string description = "Comment Here.";

    public GameObject npc_gameobject;

    //The max range of this NPC's health.
    [Range(0f, 100000f)]
    public float health;
    [Range(0f, 10f)]
    public float speed;
    [Range(0f, 1000f)]
    public float field_vision;

    // Start is called before the first frame update
    void Start()
    {
        npc_gameobject = this.GameObject();

        if (name == null)
        {
            Debug.LogWarning("NPC_ScriptableObject is missing a reference for name");
        }

        if (id == null)
        {
            Debug.LogWarning("NPC_ScriptableObject is missing a reference for idNumber");
        }

        npc_gameobject.name = name;
    }
}
