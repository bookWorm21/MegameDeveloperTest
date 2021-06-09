using Assets.Scripts.Inputing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Ship
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] private ShipShooting _shipShooting;
        [SerializeField] private ShipMovable _shipMovable;
        [SerializeField] private CurrentInput _currentInput;

        private void OnEnable()
        {
            _currentInput.ChangedInputing += OnChangeInputting;
        }

        private void OnDisable()
        {
            _currentInput.ChangedInputing -= OnChangeInputting;
        }

        private void OnChangeInputting(ShipInputting inputting)
        {
            _shipMovable.SetInputting(inputting);
            _shipShooting.SetInputting(inputting);
        }
    }
}