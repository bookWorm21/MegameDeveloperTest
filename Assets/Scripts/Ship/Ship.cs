using Assets.Scripts.Inputing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Ship
{
    public class Ship : MonoBehaviour, IDamagable
    {
        [SerializeField] private ShipShooting _shipShooting;
        [SerializeField] private ShipMovable _shipMovable;
        [SerializeField] private CurrentInputSource _currentInput;
        [SerializeField] private int _health;

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

        public void ApplyDamage(int damage)
        {
            throw new System.NotImplementedException();
        }
    }
}