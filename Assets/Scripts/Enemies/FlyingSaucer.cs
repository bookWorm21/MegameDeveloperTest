using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class FlyingSaucer : MonoBehaviour, IDamagable
    {
        [SerializeField] private float _speed;
        [SerializeField] private int _damage;
        [SerializeField] private int _pointForKill;
        [SerializeField] private float _minTimeShootColdown;
        [SerializeField] private float _maxTimeshootColdown;
        [SerializeField] private Shooting _shooting;

        private Vector3 _moveDirection;
        private float _currentShootColdown;
        private float _elapsedTime;
        private Ship.Ship _ship;

        public int PointForKill => _pointForKill;

        public event System.Action<int> Destroed;

        private void Start()
        {
            _currentShootColdown = Random.Range(_minTimeShootColdown, _maxTimeshootColdown);
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
        }

        public void ApplyDamage(int damage)
        {
            Destroed?.Invoke(_pointForKill);
            gameObject.SetActive(false);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.TryGetComponent(out Ship.Ship ship))
            {
                ship.ApplyDamage(_damage);
            }
        }
    }
}