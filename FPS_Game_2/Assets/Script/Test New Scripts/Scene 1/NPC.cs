using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS_Game
{
    public class NPC : Character
    {
        internal override void OnEnable()
        {
            base.OnEnable();
            transform.tag = "NPC";
        }

        internal override void Update()
        {
            base.Update();
        }

        internal override void LateUpdate()
        {
            base.LateUpdate();
        }
    }
}
