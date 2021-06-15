using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private int _damage;

        private float _maxFlyingDistance;

        private Shooting _parent;
        private Vector3 _moveDirection;
        private float _liveTime;
        private float _elapsedTime;

        public event System.Action<Bullet> Destroed;

        private void Start()
        {
            _maxFlyingDistance = GameMap.MapWight;
            _liveTime = _maxFlyingDistance / _speed;
        }

        public void Init(Shooting parent, Vector3 moveDirection)
        {
            _elapsedTime = 0;
            _parent = parent;
            _moveDirection = moveDirection;
        }

        private void Update()
        {
            _elapsedTime += Time.deltaTime;
            if(_elapsedTime > _liveTime)
            {
                _elapsedTime = 0;
                Destroed?.Invoke(this);
                gameObject.SetActive(false);
            }

            transform.position = Vector3.MoveTowards(transform.position, transform.position + _moveDirection,
                                                     _speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IDamagable damagable))
            {
                if (other.gameObject.TryGetComponent(out Shooting shooting))
                {
                    if(shooting != _parent)
                    {
                        damagable.ApplyDamage(_damage);
                        Destroed?.Invoke(this);
                        gameObject.SetActive(false);
                    }
                }
                else
                {
                    damagable.ApplyDamage(_damage);
                    Destroed?.Invoke(this);
                    gameObject.SetActive(false);
                }
            }
        }
    }
}