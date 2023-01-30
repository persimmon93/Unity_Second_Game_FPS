using UnityEngine;
using UnityEngine.UI;

public class HealthBase : MonoBehaviour
{
    /// <summary>
    /// Class that will be attached to all objects requiring health.
    /// </summary>
    /// 
    protected float _health;

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

    }

    /// <summary>
    /// Changes health of object by amount.
    /// </summary>
    /// <param name="amount"></param> Amount of health to be added/removed.
    public virtual void ChangeHealth(float amount)
    {
        _health += amount;
    }

    protected virtual float SetInitialHealth()
    {
        return 100f;
    }
}
