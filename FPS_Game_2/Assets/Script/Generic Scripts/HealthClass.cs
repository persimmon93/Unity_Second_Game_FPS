using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.InputSystem;
using UnityEngine;

//This Class should handle all operations related to health.
public class HealthClass : MonoBehaviour
{
    private float health;
    private float maxHealth;

    //Health will not go above maxHealth or below 0.
    public virtual void ChangeHealth(float amount)
    {
        if (health + amount > maxHealth)
        {
            health = maxHealth;
        } else if (health + amount <= 0)
        {
            health = 0;
        } else
        {
            health += amount;
        }
    }

    public void SetMaxHealth(float amount)
    {
        if (amount <= 0)
        {
            Debug.LogWarning("SetMaxHealth method was ran with a parameter that is a negative number.");
        } else
        {
            maxHealth = amount;
        }
    }

    public void ResetHealth()
    {
        health = maxHealth;
    }
    public float GetHealth()
    {
        return health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }
}
