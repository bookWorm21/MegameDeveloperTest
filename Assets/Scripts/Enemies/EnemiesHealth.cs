using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class EnemiesHealth : MonoBehaviour, IDamagable
    {
        [SerializeField] private Transform _main;

        private int _collisieDamage;

        public Transform Main => _main;

        public event System.Action Damaged;
        public event System.Action Collisied;

        public void ApplyDamage(int damage)
        {
            Damaged?.Invoke();
        }

        private void OnCollisionEnter(Collision collision)
        {
            Collisied?.Invoke();
            if(collision.gameObject.TryGetComponent(out IDamagable damagable))
            {
                damagable.ApplyDamage(_collisieDamage);
            }
        }
    }
}