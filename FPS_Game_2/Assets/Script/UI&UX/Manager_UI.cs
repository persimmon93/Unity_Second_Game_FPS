using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class will handle all the functions relating to UI. 
/// </summary>

namespace UserInterface
{
    public class Manager_UI : MonoBehaviour
    {
        public GameObject startingReference;    //This is the starting point where it will get child from.
        public Data_UI[] referenceManager;

        public Gradient gradient;   //Set gradient for health sliders to this.


        /// <summary>
        /// ReferenceManager[0] = TargetNPC (For looking at npc)
        /// ReferenceManager[1] = TargetItem (For looking at item)
        /// ReferenceManager[2] = PlayerInfo (For player health, item, ammo count.)
        /// </summary>
        private void Awake()
        {
            if (startingReference == null)
                Debug.LogError("Missing startingReference for gameObject starting startingReference for " + transform.name + ".");
            referenceManager = new Data_UI[startingReference.transform.childCount];
            int i = 0;
            foreach (Transform child in startingReference.transform)
            {
                if (referenceManager[i] == null)
                {
                    referenceManager[i] = child.GetComponent<Data_UI>();
                }
                i++;
            }
        }


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// ReferenceManager[0] = TargetNPC (For looking at npc)
        /// TargetInfo contains the array containing these values of gameObject.
        /// referenceData[0] = "Target Name". GetComponent: TMP_Text and update name for npc.
        /// referenceData[1] = "Target Health". GetComponent: Slider and update health of npc.
        /// </summary>
        public void TargetNPC(RaycastHit target)
        {
            Data_UI pointer = referenceManager[0];
            //Checks to make sure reference to TargetNPC is not null.
            if (pointer == null)
                Debug.LogError("Missing reference for TargetNPC");
            //Target not Found
            if (target.transform.GetComponent<MainClass_NPC>() == null)
            {
                pointer.transform.gameObject.SetActive(false);
                return;
            }
            //Target found
            pointer.transform.gameObject.SetActive(true);
            MainClass_NPC targetInfo = target.transform.GetComponent<MainClass_NPC>();
            //Setting name of target NPC.
            pointer.referenceData[0].transform.GetComponent<TMP_Text>().text =
                targetInfo.name;
            //Setting Health of target NPC.
            pointer.referenceData[1].GetComponent<Slider>().value =
                targetInfo.npcHealth.GetHealth();
            pointer.referenceData[1].GetComponent<Slider>().maxValue =
                targetInfo.npcHealth.GetMaxHealth();
            //Sets the gradient for health slider
            pointer.referenceData[1].transform.Find("Fill Area").GetComponentInChildren<Image>().color =
                gradient.Evaluate(pointer.referenceData[1].GetComponent<Slider>().normalizedValue);
        }

        /// <summary>
        /// ReferenceManager[1] = TargetItem (For looking at item)
        /// TargetItem contains the array containing these values of gameObject.
        /// referenceData[0] = "Pick Up Info". Display for player. Does not need to be changed.
        /// referenceData[1] = "Item Name". GetComponent: TMP_Text and update name for item.
        /// referenceData[2] = "Item Description". GetComponent: TMP_Text and update descriptioon of item.
        /// referenceData[3] = "Ammo Count". GetComponent: TMP_Text and update descriptioon of item.
        /// </summary>
        public void TargetItem(RaycastHit target)
        {
            Data_UI pointer = referenceManager[1];
            //Checks to make sure reference to TargetNPC is not null.
            if (pointer == null)
                Debug.LogError("Missing reference for TargetItem");

            //Target not Found
            if (!target.transform.GetComponent<Interactable>())
            {
                pointer.transform.gameObject.SetActive(false);
                return;
            }

            //Target found
            pointer.transform.gameObject.SetActive(true);
            GunClass targetItem = target.transform.GetComponent<GunClass>();
            //Setting name of item.
            pointer.referenceData[1].transform.GetComponent<TMP_Text>().text =
                targetItem.name;
            //Setting description of item.
            pointer.referenceData[2].transform.GetComponent<TMP_Text>().text =
                targetItem.description;
            //Setting ammo count of item.
            pointer.referenceData[3].transform.GetComponent<TMP_Text>().text =
                targetItem.currentAmmoCount.ToString() + " / " + targetItem.maxAmmoCount.ToString();
        }

        /// <summary>
        /// ReferenceManager[2] = PlayerInfo (For UI/UX for player)
        /// PlayerInfo contains the array containing these values of gameObject.
        /// referenceData[0] = "Weapon Icon". GetComponent: Image and place GunClass sprite into here, null if null.
        /// referenceData[1] = "Ammo Icon". Is Display image. Do not change.
        /// referenceData[2] = "Health Icon". Is Display image. Do not change.
        /// referenceData[3] = "Equipped Weapon". GetComponent: TMP_Text and place GunClass name here, null if null.
        /// referenceData[4] = "Ammo". GetComponent: TMP_Text and place GunClass ammo count here, null if null.
        /// referenceData[5] = "Number Health". Get Component: TMP_Text and place HealthClass health value here.
        /// referenceData[6] = "Slider Health". GetComponent: Slider and place HealthClass health value here.
        /// //This method will use referenceData: 5 and 6.
        /// This should run whenever the max health changes or initiaizing the health value at start.
        /// </summary>
        public void PlayerInfoSetHealth(HealthClass health)
        {
            Data_UI pointer = referenceManager[2];
            //Checks to make sure reference to PlayerInfo is not null.
            if (pointer == null)
                Debug.LogError("Missing reference for PlayerInfo");

            //HealthClass not Found
            if (health == null)
                return;

            //HealthClass found
            //Setting Number Health of player.
            pointer.referenceData[5].transform.GetComponent<TMP_Text>().text =
                health.GetHealth() + " / " + health.GetMaxHealth();

            //Setting Slider Health of player.
            pointer.referenceData[6].transform.GetComponent<Slider>().maxValue =
                health.GetMaxHealth();
            pointer.referenceData[6].transform.GetComponent<Slider>().value =
                health.GetHealth();
            //Sets the gradient for health slider
            pointer.referenceData[6].transform.Find("Fill Area").GetComponentInChildren<Image>().color =
                gradient.Evaluate(pointer.referenceData[6].GetComponent<Slider>().normalizedValue);
        }

        //This is a quick call to update health as it changes for player.
        public void PlayerInfoQuickChangeHealth(HealthClass healthChanged)
        {
            Data_UI pointer = referenceManager[2];
            //Checks to make sure reference to PlayerInfo is not null.
            if (pointer == null)
                Debug.LogError("Missing reference for PlayerInfo");

            //HealthClass not Found
            if (healthChanged == null)
                return;

            //Setting Slider Health of player.
            pointer.referenceData[6].transform.GetComponent<Slider>().value =
                healthChanged.GetHealth();
            //Sets the gradient for health slider (May not need.)
            pointer.referenceData[6].transform.Find("Fill Area").GetComponentInChildren<Image>().color =
                gradient.Evaluate(pointer.referenceData[6].GetComponent<Slider>().normalizedValue);
        }

        /// <summary>
        /// ReferenceManager[2] = PlayerInfo (For UI/UX for player)
        /// PlayerInfo contains the array containing these values of gameObject.
        /// referenceData[0] = "Weapon Icon". GetComponent: Image.sprite and place GunClass sprite into here, null if null.
        /// referenceData[1] = "Ammo Icon". Is Display image. Do not change.
        /// referenceData[2] = "Health Icon". Is Display image. Do not change.
        /// referenceData[3] = "Equipped Weapon". GetComponent: TextMeshPro and place GunClass name here, null if null.
        /// referenceData[4] = "Ammo". GetComponent: TextMeshPro and place GunClass ammo count here, null if null.
        /// referenceData[5] = "Number Health". Get Component: TextMeshPro and place HealthClass health value here.
        /// referenceData[6] = "Slider Health". GetComponent: Slider and place HealthClass health value here.
        /// //This method will use referenceData: 0, 1, 3, 4
        /// </summary>
        public void PlayerInfoSetWeapon(GameObject item)
        {
            Data_UI pointer = referenceManager[2];
            //Checks to make sure reference to PlayerInfo is not null.
            if (pointer == null)
                Debug.LogError("Missing reference for PlayerInfo");

            //GunClass not Found, set active false.
            if (item == null)
            {
                pointer.referenceData[0].SetActive(false);
                pointer.referenceData[1].SetActive(false);
                pointer.referenceData[3].SetActive(false);
                pointer.referenceData[4].SetActive(false);
                return;
            }

            if (item.GetComponent<GunClass>())
            {
                //GunClass found
                pointer.referenceData[0].SetActive(true);
                pointer.referenceData[1].SetActive(true);
                pointer.referenceData[3].SetActive(true);
                pointer.referenceData[4].SetActive(true);
                GunClass weaponTemp = item.transform.GetComponent<GunClass>();
                //Setting Weapon Icon
                pointer.referenceData[0].transform.GetComponent<Image>().sprite =
                    weaponTemp.gunSprite;
                //Setting Equipped Weapon name.
                pointer.referenceData[3].transform.GetComponent<TMP_Text>().text =
                    weaponTemp.name;
                //Setting Equipped Weapon ammo.
                //pointer.referenceData[4].transform.GetComponent<TMP_Text>().text =
                    //weaponTemp.currentAmmoCount + " | " +
                   // item.transform.GetComponentInParent<InventoryClass>().GetAmmoCount(item.GetComponent<WeaponType>());
            }
        }
    }
}