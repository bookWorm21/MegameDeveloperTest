using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Inputing
{
    public class OnlyKeyboardInput : MonoBehaviour, IShipInputting
    {
        private bool _needBoost;
        private bool _needShoot;
        private float _rotation;

        private void Update()
        {
            _needShoot = Input.GetKeyDown(KeyCode.Space);
            _needBoost = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
            _rotation = Input.GetAxis("Horizontal");
        }

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