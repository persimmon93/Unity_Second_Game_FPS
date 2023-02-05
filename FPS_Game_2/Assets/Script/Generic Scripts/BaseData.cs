using System;
using UnityEngine;
using UnityEngine.UI;

public class BaseData : MonoBehaviour
{
    /// <summary>
    /// Script that will be inherited by player and npcs that will contain basic information
    /// necessary for game play.
    /// </summary>
    /// 

    //Class set to protected so that it can be accessible and modified in class inheriting this.
    protected float _health;
    protected float _stamina;
    protected float _maxHealth;
    protected float _maxStamina;

    protected bool isDead;




    [Header("Setting")]
    [Tooltip("If true, destroys game object when health reaches 0")]
    protected bool destroyUponDeath;

    /// <summary>
    /// If active, Health should override to display health slider.
    /// </summary>
    public virtual void DisplayHealth()
    {

    }

    /// <summary>
    /// If boolean is true, will destroy gameOject upon health reaching below 0.
    /// </summary>
    public virtual void Death()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Changes health of object by amount.
    /// </summary>
    /// <param name="amount"></param> Amount of health to be added/removed.
    public virtual void ChangeHealth(float amount)
    {
        _health += amount;
    }

    public virtual void ChangeStamina(float amount)
    {
        _stamina += amount;
    }

    protected void ChangeMaximumHealth(float amount)
    {
        _maxHealth += amount;
    }

    protected void ChangeMaximumStamina(float amount)
    {
        _maxStamina += amount;
    }


    protected void SetInitialHealth(float amount)
    {
        _health = amount;
    }

    protected void SetInitialStamina(float amount)
    {
        _stamina = amount;
    }
}
