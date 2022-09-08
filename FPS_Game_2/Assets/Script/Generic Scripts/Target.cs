using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

/// <summary>
/// Script that makes game objeect with this script targetable for action.
/// </summary>
public class Target : MonoBehaviour
{
    public float health;

    // Start is called before the first frame update
    void Start()
    {
        if (health <= 0)
            health = 100f;
    }

    private void Update()
    {
        //For now, it will destroy but later change it to ragdoll.
        if (health <= 0)
            Destroy(transform.gameObject);
    }
}
