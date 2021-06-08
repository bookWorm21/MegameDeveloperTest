using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Inputing
{
    public interface IShipInputting
    {
        public bool NeedShoot();

        public bool NeedBoost();

        public float Rotation();
    }
}