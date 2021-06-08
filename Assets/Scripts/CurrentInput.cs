using Assets.Scripts.Inputing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class CurrentInput : MonoBehaviour
    {
        [SerializeField] private OnlyKeyboardInput _onlyKeyboardInput;

        public event System.Action<IShipInputting> ChangedInputing;

        private void Start()
        {
            ChangedInputing?.Invoke(_onlyKeyboardInput);
        }
    }
}