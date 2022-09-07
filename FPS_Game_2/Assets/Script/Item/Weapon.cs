using UnityEngine;

/// <summary>
/// This script will find the tag of the game object that contains this script and attribute data
/// according to the tag. 
/// It will also contain an attack function necessary for a weapon.
/// </summary>
public class Weapon : MonoBehaviour
{
    [SerializeField] private bool weaponIdentity; //false by default. Made to run once to identity gun type.

    public float damage;
    public float range;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!weaponIdentity)
        {
            switch (gameObject.tag)
            {
                case "Pistol":
                    damage = 20f;
                    range = 100f;
                    break;
                case "Rifle":
                    damage = 50f;
                    range = 200f;
                    break;
                case "Sniper":
                    damage = 100f;
                    range = 300f;
                    break;
                default:
                    //Melee Weapon
                    damage = 10f;
                    range = 5f;
                    break;
            }
            weaponIdentity = true;
        }
    }

    internal void Attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

        }
    }
}
