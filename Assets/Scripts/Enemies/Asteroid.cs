using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class Asteroid : MonoBehaviour
    {
        [Header("angle")]
        [SerializeField] private float _childDeltaMoveDirection;

        [Space(5)]
        [SerializeField] private int _pointForKill;
        [SerializeField] private int _childCount;

        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;

        [SerializeField] private int _damage;
        [SerializeField] private EnemiesHealth _body;

        [SerializeField] private AudioSource _explosion;

        private Vector3 _moveDirection;
        private Asteroid[] _childs;
        private float _currentSpeed;
        private int _destroedChilds = 0;

        private WaitForSeconds _disactivateWait;

        private static Vector3 _horizontal = new Vector3(1, 0, 0);

        public bool IsActive { get; private set; }

        public int PointForKill => _pointForKill;

        public int ChildCount => _childCount;

        /// <summary>
        /// event trigg when asteriod or his childs destroed
        /// parametr type int show how many points will get player
        /// parametr type bool is show all childs destroed
        /// </summary>
        public event System.Action<int> PartDestroed;

        public event System.Action<Asteroid> FullDestroed;

        private void Start()
        {
            _body.Damaged += OnDamage;
            _body.Collisied += OnCollisie;
            _disactivateWait = new WaitForSeconds(_explosion.clip.length);
        }

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
                child.FullDestroed += OnChildDestroy;
                child.PartDestroed += OnPartDestroy;
            }
        }

        public void Activate()
        {
            _body.gameObject.SetActive(true);
            enabled = true;

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
            _body.gameObject.SetActive(true);
            enabled = true;

            _destroedChilds = 0;
            IsActive = true;

            _moveDirection = direction;
            _currentSpeed = speed;
        }

        private IEnumerator Disactivate()
        {
            yield return _disactivateWait;
            gameObject.SetActive(false);
        }

        private void OnDamage()
        {
            if (_childCount == 0)
            {
                PartDestroed?.Invoke(_pointForKill);
                FullDestroed?.Invoke(this);
            }
            else
            {
                PartDestroed?.Invoke(_pointForKill);
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

            _explosion.Play();
            _body.gameObject.SetActive(false);
            enabled = false;
            StartCoroutine(Disactivate());
        }

        private void OnPartDestroy(int points)
        {
            PartDestroed?.Invoke(points);
        }

        private void OnChildDestroy(Asteroid asteroid)
        {
            _destroedChilds++;
            if (_destroedChilds >= _childCount)
            {
                gameObject.SetActive(false);
                IsActive = false;
                _destroedChilds = 0;
                FullDestroed?.Invoke(this);
            }
        }

        private void OnCollisie()
        {
            _explosion.Play();
            _body.gameObject.SetActive(false);
            enabled = false;

            IsActive = false;
            _destroedChilds = 0;

            PartDestroed?.Invoke(_pointForKill);
            FullDestroed?.Invoke(this);

            StartCoroutine(Disactivate());
        }
    }
}