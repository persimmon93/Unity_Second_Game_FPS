using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeClass : MonoBehaviour
{
    [SerializeField] float explosiveForce = 5000f;
    [SerializeField] float explodeRadius = 100f;
    [SerializeField] bool activate = false;

    private void OnEnable()
    {
        

    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.N))
        {
            activate = true;
            Explode();
        }
    }
    public void Explode()
    {
        if (activate)
        {
            Debug.Log("Exploded");
            Collider[] colliders = Physics.OverlapSphere(transform.position, explodeRadius);

            foreach (Collider nearbyObject in colliders)
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(explosiveForce, transform.position, explodeRadius);
                }
            }

            Destroy(gameObject);
        }
    }
}
