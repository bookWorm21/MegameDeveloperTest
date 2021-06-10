using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class Asteroid : MonoBehaviour, IDamagable
    {
        [SerializeField] private int _pointForDie;
        [SerializeField] private int _childCount;

        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;

        private Vector3 _moveDirection;
        private Asteroid[] _childs;
        private float _currentSpeed;
        private int _destroedChilds;

        public bool IsActive { get; private set; }

        public bool IsMainPart { get; private set; }

        public int ChildCount => _childCount;

        /// <summary>
        /// event triggers when asteriod destroy
        /// paramert shows whether the entire asteriod was destroyed along with its descendants
        /// </summary>
        public event System.Action<bool> Destroed;

        private void Start()
        {
                
        }

        public void SetChilds(Asteroid[] childs)
        {
            _childs = childs;
        }

        public void Activate(bool isMainPart)
        {
            _destroedChilds = 0;
            IsMainPart = isMainPart;
            IsActive = true;
            _moveDirection = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);
            _moveDirection = _moveDirection.normalized;
            _currentSpeed = Random.Range(_minSpeed, _maxSpeed);
        }

        public void Activate(bool isMainPart, Vector3 direction, float speed)
        {
            _destroedChilds = 0;
            IsMainPart = isMainPart;
            IsActive = true;
            _moveDirection = direction;
            _currentSpeed = speed;
        }

        public void ApplyDamage(int damage)
        {
            
        }

        private void OnChildDestroy(bool destroedRoot)
        {

        }

        private void OnCollisionEnter(Collision collision)
        {
            
        }
    }
}