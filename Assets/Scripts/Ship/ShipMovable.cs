using Assets.Scripts.Inputing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Ship
{
    public class ShipMovable : MonoBehaviour
    {
        [SerializeField] private CurrentInput _currentInput;

        [SerializeField] private Vector3 _rotationDirection;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _airDrag;

        [SerializeField] private Transform _head;
        [SerializeField] private Transform _root;

        private IShipInputting _inputting;

        private Vector3 _currentVelocity;
        private Vector3 _currentDirection;

        private void OnEnable()
        {
            _currentInput.ChangedInputing += OnChangeInputting;
        }

        private void Start()
        {
            _rotationDirection = _rotationDirection.normalized;
        }

        private void Update()
        {
            transform.eulerAngles += _rotationDirection * _rotationSpeed * _inputting.Rotation() * Time.deltaTime;

            _currentDirection = (_head.position - _root.position).normalized;
            _currentVelocity += Time.deltaTime * _currentDirection * (_inputting.NeedBoost() ? _acceleration : 0);
            if (_currentVelocity.magnitude > 0)
            {
                //_currentVelocity -= Time.deltaTime * _currentVelocity * _airDrag;
                _currentVelocity = Vector3.ClampMagnitude(_currentVelocity, _maxSpeed);
            }
            transform.position = Vector3.MoveTowards(transform.position, transform.position + _currentVelocity,
                                                     Time.deltaTime);
        }

        private void OnDisable()
        {
            _currentInput.ChangedInputing -= OnChangeInputting;    
        }

        private void OnChangeInputting(IShipInputting inputting)
        {
            _inputting = inputting;
        }
    }
}