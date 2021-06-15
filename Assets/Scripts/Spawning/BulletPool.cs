using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Spawning
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private Bullet _preafab;
        [SerializeField] private int _capacity;

        private Queue<Bullet> _bulletsQueue = new Queue<Bullet>();

        public void Initialize()
        {
            for(int i = 0; i < _capacity; i++)
            {
                Bullet bullet = Instantiate(_preafab, _container);
                bullet.Destroed += OnBulletDestroy;
                bullet.gameObject.SetActive(false);
                _bulletsQueue.Enqueue(bullet);
            }
        }

        public Bullet GetBullet()
        {
            if (_bulletsQueue.Count > 0)
            {
                return _bulletsQueue.Dequeue();
            }
            else
            {
                Bullet bullet = Instantiate(_preafab, _container);
                bullet.Destroed += OnBulletDestroy;
                bullet.gameObject.SetActive(false);
                return bullet;
            }
        }

        private void OnBulletDestroy(Bullet bullet)
        {
            _bulletsQueue.Enqueue(bullet);
        }
    }
}