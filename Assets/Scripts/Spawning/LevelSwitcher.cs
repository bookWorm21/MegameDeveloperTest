using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Spawning
{
    public class LevelSwitcher : MonoBehaviour
    {
        [SerializeField] private AsteroidPool _asteroidPool;
        [SerializeField] private AsteroidSpawner _asteroidSpawner;
        [Min(1)]
        [SerializeField] private int _startAsteroidCount;
        [Min(1)]
        [SerializeField] private int _deltaAsteroidsBetweenLevel;
        [SerializeField] private float _delayBetweenSpawnAsteroids;

        private int _currentAsteroidCount;
        private int _currentDestroedAsteriod = 0; 
        private WaitForSeconds _delay;

        private void Start()
        {
            _currentAsteroidCount = _startAsteroidCount;
            _delay = new WaitForSeconds(_delayBetweenSpawnAsteroids);
            _asteroidPool.Initialize();

            for (int i = 0; i < _asteroidPool.GetAsteroidsSize; i++)
            {
                _asteroidPool.GetAsteroid(i).Destroed += OnAsteroidDestroy;
            }

            StartCoroutine(SpawnAsteroidWithDelay());
        }

        private void OnDisable()
        {
            for (int i = 0; i < _asteroidPool.GetAsteroidsSize; i++)
            {
                _asteroidPool.GetAsteroid(i).Destroed -= OnAsteroidDestroy;
            }
        }

        private void OnAsteroidDestroy()
        {
            _currentDestroedAsteriod++;
            if (_currentDestroedAsteriod == _currentAsteroidCount)
            {
                _currentDestroedAsteriod = 0;
                _currentAsteroidCount += _deltaAsteroidsBetweenLevel;
                StartCoroutine(SpawnAsteroidWithDelay());
            }
        }

        private IEnumerator SpawnAsteroidWithDelay()
        {
            yield return _delay;
            _asteroidSpawner.SpawnAsteroids(_currentAsteroidCount);
        }
    }
}