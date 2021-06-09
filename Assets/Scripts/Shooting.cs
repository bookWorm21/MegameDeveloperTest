using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Shooting : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _shootPoint;

        public void Shoot(Vector3 shootDirection)
        {
            Bullet bullet = Instantiate(_bulletPrefab, _shootPoint.position, Quaternion.identity);
            bullet.Init(this, shootDirection);
        }
    }
}