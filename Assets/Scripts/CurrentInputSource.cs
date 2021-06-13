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

            for(int i = 0; i < _inputtings.Length; i++)
            {
                _inputtings[i].enabled = false;
            }

            _currentInputtingIndex = 0;
            _currentInputting = _inputtings[_currentInputtingIndex];
            EnableInput();
            ChangedInputing?.Invoke(_currentInputting);
        }

        public void EnableInput()
        {
            _inputtings[_currentInputtingIndex].enabled = true;
        }

        public void DisableInput()
        {
            _inputtings[_currentInputtingIndex].enabled = false;
        }

        public void ChangeInputting()
        {
            _inputtings[_currentInputtingIndex].enabled = false;
            _currentInputtingIndex++;
            if(_currentInputtingIndex >= _inputtings.Length)
            {
                _currentInputtingIndex = 0;
            }

            _currentInputting = _inputtings[_currentInputtingIndex];
            _inputtings[_currentInputtingIndex].enabled = true;
            ChangedInputing?.Invoke(_currentInputting);
        }
    }
}