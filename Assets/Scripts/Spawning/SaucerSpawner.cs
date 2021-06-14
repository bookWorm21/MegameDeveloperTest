using Assets.Scripts.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Spawning
{
    public class SaucerSpawner : MonoBehaviour
    { 
        [Header("In %")]
        [SerializeField] private int _offsetToScreenBorder;

        [SerializeField] private float _minTimeSpawnDelay;
        [SerializeField] private float _maxTimeSpawnDelay;

        [SerializeField] private FlyingSaucer _saucerPrefab;
        [SerializeField] private Ship.Ship _ship;

        private FlyingSaucer _saucerInScene;

        public event System.Action<int> SaucerDestroed;

        private void Awake()
        {
            _saucerInScene = Instantiate(_saucerPrefab);

            _saucerInScene.Destroed += (int points) => SaucerDestroed?.Invoke(points);

            _saucerInScene.gameObject.SetActive(false);
            _saucerInScene.SetShip(_ship);
            StartCoroutine(Spawn());
        }

        private void OnEnable()
        {
            _saucerInScene.Destroed += OnSaucerDestroy;
        }

        private void OnDisable()
        {
            _saucerInScene.Destroed -= OnSaucerDestroy;
        }

        private void OnSaucerDestroy(int point)
        {
            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            yield return new WaitForSeconds(Random.Range(_minTimeSpawnDelay, _maxTimeSpawnDelay));

            Vector3 moveDirection;
            float fullnessHeightRange = (100 - _offsetToScreenBorder) / 100.0f;
            float startX, startY;
            if(Random.value > 0.5f)
            {
                startX = GameMap.MinX;
                moveDirection = new Vector3(1, 0, 0);
            }
            else
            {
                startX = GameMap.MaxX;
                moveDirection = new Vector3(-1, 0, 0);
            }

            startY = Random.Range(GameMap.MinY * fullnessHeightRange,
                                  GameMap.MaxY * fullnessHeightRange);

            _saucerInScene.transform.position = new Vector3(startX, startY, 0);
            _saucerInScene.Activate(moveDirection);
        }
    }
}