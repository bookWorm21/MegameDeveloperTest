using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class Asteroid : MonoBehaviour, IDamagable
    {
        [Header("angle")]
        [SerializeField] private float _childDeltaMoveDirection;

        [SerializeField] private int _pointForKill;
        [SerializeField] private int _childCount;

        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;

        [SerializeField] private int _damage;

        private Vector3 _moveDirection;
        private Asteroid[] _childs;
        private float _currentSpeed;
        private int _destroedChilds = 0;

        private static Vector3 _horizontal = new Vector3(1, 0, 0);

        public bool IsActive { get; private set; }

        public int PointForKill => _pointForKill;

        public int ChildCount => _childCount;

        /// <summary>
        /// event trigg when asteriod or his childs destroed
        /// parametr type int show how many points will get player
        /// parametr type bool is show all childs destroed
        /// </summary>
        public event System.Action<int, bool> PartDestroed;

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + _moveDirection,
                                                     _currentSpeed * Time.deltaTime);
        }

        public void SetChilds(Asteroid[] childs)
        {
            _childs = childs;
            foreach(var child in childs)
            {
                child.PartDestroed += OnChildDestroy;
            }
        }

        public void Activate()
        {
            _destroedChilds = 0;
            IsActive = true;

            float directionX = Random.Range(-1.0f, 1.0f);
            float directionY;

            if(directionX != 0)
            {
                directionY = Random.Range(-1.0f, 1.0f);
            }
            else
            {
                directionY = Random.Range(0.1f, 1) * (Random.value > 0.5f ? 1 : -1);
            }

            _moveDirection = new Vector3(directionX, directionY, 0);
            _moveDirection = _moveDirection.normalized;
            _currentSpeed = Random.Range(_minSpeed, _maxSpeed);
        }

        public void Activate(Vector3 direction, float speed)
        {
            _destroedChilds = 0;
            IsActive = true;
            _moveDirection = direction;
            _currentSpeed = speed;
        }

        public void ApplyDamage(int damage)
        {
            gameObject.SetActive(false);

            if (_childCount == 0)
            {
                PartDestroed?.Invoke(_pointForKill, true);
                return;
            }
            else
            {
                PartDestroed?.Invoke(_pointForKill, false);
            }

            float baseAngle = Vector3.Angle(_horizontal, _moveDirection);
            Vector3 childMoveDirection;

            if(_moveDirection.y < 0)
            {
                baseAngle *= -1;
            }

            float childSpeed = Random.Range(_minSpeed, _maxSpeed);
            float leftBorder = baseAngle - _childDeltaMoveDirection;
            float deltaAngle;

            if(_childCount > 1)
            {
                deltaAngle = _childDeltaMoveDirection * 2 / (_childCount - 1);
            }
            else
            {
                deltaAngle = leftBorder;
            }

            leftBorder = leftBorder / 180.0f * Mathf.PI;
            deltaAngle = deltaAngle / 180.0f * Mathf.PI;

            for(int i = 0; i < _childCount; i++, leftBorder += deltaAngle)
            {
                childMoveDirection = new Vector3(Mathf.Cos(leftBorder), Mathf.Sin(leftBorder), 0);
                _childs[i].Activate(childMoveDirection, childSpeed);
                _childs[i].gameObject.transform.position = transform.position;
                _childs[i].gameObject.transform.rotation = transform.rotation;
                _childs[i].gameObject.SetActive(true);
            }
        }

        private void OnChildDestroy(int points, bool isFullDestroy)
        {
            PartDestroed?.Invoke(points, false);
            if (isFullDestroy)
            {
                _destroedChilds++;
                if (_destroedChilds >= _childCount)
                {
                    gameObject.SetActive(false);
                    IsActive = false;
                    _destroedChilds = 0;
                    PartDestroed?.Invoke(_pointForKill, true);
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            gameObject.SetActive(false);
            IsActive = false;
            _destroedChilds = 0;

            PartDestroed?.Invoke(_pointForKill, true);

            if (collision.gameObject.TryGetComponent(out Ship.Ship ship))
            {
                ship.ApplyDamage(_damage);
            }
            else if (collision.gameObject.TryGetComponent(out FlyingSaucer saucer))
            {
                saucer.ApplyDamage(_damage);
            }
        }
    }
}