using Unity.VisualScripting;
using UnityEngine;


public class Interactable : MonoBehaviour
{
    /// <summary>
    /// Place in gameobjects to make able to pick up.
    /// </summary>
    [SerializeField] private bool canPickUp = true;
    [SerializeField] private float interactDistance = 1f;
    private GameObject interactingGameObject;

    private void OnEnable()
    {
        interactingGameObject = transform.gameObject;
    }
    public GameObject InteractingObject(float distanceFromRaycastHit)
    {
        if (!canPickUp)
            return null;

        //If Object is futher than interacting distance, it should not be interactable.
        if (distanceFromRaycastHit >= interactDistance)
            return null;

        return interactingGameObject;
    }

    public void ToggleCanPickUp(bool setting)
    {
        canPickUp = setting;
    }
}
