using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class FlyingSaucer : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private int _damage;
        [SerializeField] private int _pointForKill;
        [SerializeField] private float _minTimeShootColdown;
        [SerializeField] private float _maxTimeshootColdown;
        [SerializeField] private Shooting _shooting;
        [SerializeField] private EnemiesHealth _body;
        [SerializeField] private AudioSource _explosion;

        private Vector3 _moveDirection;
        private float _currentShootColdown;
        private float _elapsedTime;
        private Ship.Ship _ship;

        private WaitForSeconds _disactivateWait;

        /// <summary>
        /// event trigg when saucer destroy
        /// parametr type int show how many points will get player
        /// </summary>
        public event System.Action<int> Destroed;

        private void Start()
        {
            _currentShootColdown = Random.Range(_minTimeShootColdown, _maxTimeshootColdown);
            _disactivateWait = new WaitForSeconds(_explosion.clip.length);

            _body.Collisied += OnDamage;
            _body.Damaged += OnDamage;
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + _moveDirection,
                                                     _speed * Time.deltaTime);

            _elapsedTime += Time.deltaTime;
            if (_elapsedTime > _currentShootColdown)
            {
                _elapsedTime = 0;
                _currentShootColdown = Random.Range(_minTimeShootColdown, _maxTimeshootColdown);
                _shooting.Shoot((_ship.transform.position - transform.position).normalized);
            }
        }

        public void SetShip(Ship.Ship ship)
        {
            _ship = ship;
        }

        public void Activate(Vector3 moveDirection)
        {
            _moveDirection = moveDirection;
            gameObject.SetActive(true);
            _body.gameObject.SetActive(true);
            enabled = true;
        }

        private void OnDamage()
        {
            _explosion.Play();
            _body.gameObject.SetActive(false);
            enabled = false;
            StartCoroutine(Disactivate());
        }

        private IEnumerator Disactivate()
        {
            yield return _disactivateWait;
            gameObject.SetActive(false);
            Destroed?.Invoke(_pointForKill);
        }
    }
}