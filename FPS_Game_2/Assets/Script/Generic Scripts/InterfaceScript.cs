using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS_Game
{
    public interface IDamageable
    {
        void Damage(float amount);
    }

    public interface IThrowable
    {
        void Throw();
    }

    public interface IPickUpAble
    {
        GameObject PickUp();
    }

    public interface IExtractable
    {

    }
}
