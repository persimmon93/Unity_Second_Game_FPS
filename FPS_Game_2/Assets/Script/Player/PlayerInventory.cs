using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    protected List<GameObject> inventory;
    protected GameObject currentEquippedItem;
    protected float itemInteractionRange = 5f;


    //List<Item> itemList;
    public int selectedWeapon;
    // Start is called before the first frame update
    void Start()
    {
        inventory = new List<GameObject>();
        selectedWeapon = 0;
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        ScrollWeapon();
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                //Player.Instance.currentWeapon = weapon.gameObject;
            } else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }

    //This method should handle changing the selecteWeapon with scrollwheel or numbers
    void ScrollWeapon()
    {
        int previousSelectedWeapon = selectedWeapon;
        //If mouse scrollwheel up, selected weapon should increase. And if it goes beyond childcount, drops to 0.
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            selectedWeapon = (selectedWeapon >= transform.childCount - 1) ? 0 : selectedWeapon++;
        }
        //If mouse scrollwheel down, selected weapon should decrease. And if it goes below 0, it goes to total child count.
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            selectedWeapon = (selectedWeapon <= 0) ? selectedWeapon = transform.childCount - 1 : selectedWeapon--;
        }

        //PrimaryWeapon
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }
        //SecondaryWeapon
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeapon = 1;
        }
        //Knife
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedWeapon = 2;
        }
        //Grenade
        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
        {
            selectedWeapon = 3;
        }
        //Kit
        if (Input.GetKeyDown(KeyCode.Alpha5) && transform.childCount >= 5)
        {
            selectedWeapon = 5;
        }

        //Pointer for updating weapon change after scroll wheel. This should be last after all calculations are made.
        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }
}
