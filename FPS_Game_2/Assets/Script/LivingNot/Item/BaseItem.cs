using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS_Game
{
    /// <summary>
    /// All item classes will inherit this class, allowing flexible accessibility
    /// when using items.
    /// </summary>
    public abstract class BaseItem : MonoBehaviour
    {
        public string itemName;
        public string description;

        //This method runs the primary function of the item.
        public virtual void PrimaryUse()
        {

        }

        //This method runs the secondary function of the item.
        public virtual void SecondaryUse()
        {

        }
    }
}
