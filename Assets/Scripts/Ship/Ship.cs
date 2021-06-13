using Assets.Scripts.Inputing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Ship
{
    public class Ship : MonoBehaviour, IDamagable
    {
        [SerializeField] private GameObject _body;
        [SerializeField] private float _timeInvulnerabilityAfterDestroy;
        [SerializeField] private int _blinksPerSecond;

        [SerializeField] private ShipShooting _shipShooting;
        [SerializeField] private ShipMovable _shipMovable;
        [SerializeField] private CurrentInputSource _currentInput;
        [SerializeField] private int _health;
        [SerializeField] private float _respawnTime;
        [SerializeField] private float _borderSpawnSize;

        private int _blinkCount;

        private WaitForSeconds _waitBlink;
        private WaitForSeconds _waitRespawn;

        private bool _isRespawn = false;

        public event System.Action<int> ChangedHealth;

        private void OnEnable()
        {
            _currentInput.ChangedInputing += OnChangeInputting;
        }

        private void Start()
        {
            _waitRespawn = new WaitForSeconds(_respawnTime);
            _waitBlink = new WaitForSeconds(1.0f / _blinksPerSecond);
            _blinkCount = (int)(_timeInvulnerabilityAfterDestroy * _blinksPerSecond);
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
            if (_isRespawn == false)
            {
                _health--;

                ChangedHealth?.Invoke(_health);

                if (_health > 0)
                {
                    StartCoroutine(Respawn());
                }
            }
        }

        private IEnumerator Respawn()
        {
            _isRespawn = true;

            Vector3 newPosition = new Vector3(
                Random.Range(GameMap.MinX + _borderSpawnSize, GameMap.MaxX - _borderSpawnSize),
                Random.Range(GameMap.MinY + _borderSpawnSize, GameMap.MaxY - _borderSpawnSize),
                0);

            transform.position = newPosition;

            for (int elapsedBlink = 0; elapsedBlink < _blinkCount; elapsedBlink++)
            {
                _body.SetActive(!_body.activeSelf);
                yield return _waitBlink;
            }

            _body.SetActive(true);  
            _isRespawn = false;
        }
    }
}