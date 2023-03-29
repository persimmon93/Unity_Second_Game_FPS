using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FPS_Game
{
    [RequireComponent(typeof(PlayerInput))]
    /// <summary>
    /// Inherites character class and handles all functions specific to player.
    /// </summary>
    public class Player : Character, IDamageable
    {

        [HeaderAttribute("Player data")]
        float dummy;




        internal override void OnEnable()
        {
            base.OnEnable();
            transform.tag = "Player";
        }

        internal override void Update()
        {
            base.Update();
        }

        internal override void LateUpdate()
        {
            base.LateUpdate();
        }

        public void Damage(float amount)
        {
            throw new System.NotImplementedException();
        }
    }
}
