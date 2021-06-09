using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Inputing
{
    public abstract class ShipInputting : MonoBehaviour
    {
        protected bool _needBoost;
        protected bool _needShoot;
        protected float _rotation;

        public bool NeedBoost()
        {
            return _needBoost;
        }

        public bool NeedShoot()
        {
            return _needShoot;
        }

        public float Rotation()
        {
            return _rotation;
        }
    }
}