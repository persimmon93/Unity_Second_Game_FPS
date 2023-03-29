using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS_Game
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float health;
        [SerializeField] private float maxHealth;

        //Health will not go above maxHealth or below 0.
        public void ChangeHealth(float amount)
        {
            health += amount;
            health = Mathf.Clamp(health, 0, maxHealth);
        }

        public void SetMaxHealth(float amount)
        {
            if (amount <= 0)
            {
                Debug.LogWarning("SetMaxHealth method was ran with a parameter that is a negative number.");
            }
            else
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
}
