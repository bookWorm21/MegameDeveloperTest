using Assets.Scripts.Inputing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Ship
{
    public class ShipShooting : Shooting
    {
        [SerializeField] private int _bulletsPerSecond;
        [SerializeField] private ShipMovable _shitMovable;
        [SerializeField] private CurrentInputSource _currentInput;
        [SerializeField] private AudioSource _shootingAudio;

        private ShipInputting _inputting;
        private float _rechargeTime;
        private float _elapsedRechargeTime;

        private void Start()
        {
            _rechargeTime = 1.0f / _bulletsPerSecond;
            _elapsedRechargeTime = _rechargeTime;
        }

        private void Update()
        {
            _elapsedRechargeTime -= Time.deltaTime;
            if(_inputting.NeedShoot() && _elapsedRechargeTime < 0)
            {
                _elapsedRechargeTime = _rechargeTime;
                _shootingAudio.Play();
                Shoot(_shitMovable.CurrentDirection);
            }
        }

        public void SetInputting(ShipInputting inputting)
        {
            _inputting = inputting;
        }
    }
}