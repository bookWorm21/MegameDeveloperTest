using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Inputing
{
    public class OnlyKeyboardInput : ShipInputting
    {
        private void Update()
        {
            _needShoot = Input.GetKeyDown(KeyCode.Space);
            _needBoost = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
            _rotation = Input.GetAxis("Horizontal");
        }
    }
}