using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Ship.Ship _ship;
        [SerializeField] private Text _healthLabel;

        private void OnEnable()
        {
            _ship.ChangedHealth += OnHealthChange;    
        }

        private void OnDisable()
        {
            _ship.ChangedHealth -= OnHealthChange;
        }

        private void OnHealthChange(int health) 
        {
            _healthLabel.text = health.ToString();
        }
    }
}