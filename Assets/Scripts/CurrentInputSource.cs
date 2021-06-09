using Assets.Scripts.Inputing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class CurrentInputSource : MonoBehaviour
    {
        [SerializeField] private ShipInputting[] _inputtings;

        private ShipInputting _currentInputting;
        private int _currentInputtingIndex;

        public string CurrentInputtingName => _currentInputting.GetViewName();

        public event System.Action<ShipInputting> ChangedInputing;

        private void Start()
        {
            if(_inputtings.Length <= 0)
            {
                Debug.LogError("Массив inputtings не должен быть пустым");
                return;
            }

            _currentInputtingIndex = 0;
            _currentInputting = _inputtings[_currentInputtingIndex];
            ChangedInputing?.Invoke(_currentInputting);
        }

        public void ChangeInputting()
        {
            _currentInputtingIndex++;
            if(_currentInputtingIndex >= _inputtings.Length)
            {
                _currentInputtingIndex = 0;
            }

            _currentInputting = _inputtings[_currentInputtingIndex];
            ChangedInputing?.Invoke(_currentInputting);
        }
    }
}