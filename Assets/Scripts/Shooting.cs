using Assets.Scripts.Spawning;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Shooting : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private BulletPool _bulletPool;

        private void Awake()
        {
            _bulletPool.Initialize();
        }

        public void Shoot(Vector3 shootDirection)
        {
            Bullet bullet = _bulletPool.GetBullet();
            bullet.transform.position = _shootPoint.position;
            bullet.transform.rotation = Quaternion.identity;
            bullet.Init(this, shootDirection);
            bullet.gameObject.SetActive(true);
        }
    }
}