using Assets.Scripts.Enemies;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Spawning
{
    public class LevelSwitcher : MonoBehaviour
    {
        [SerializeField] private AsteroidPool _asteroidPool;
        [SerializeField] private AsteroidSpawner _asteroidSpawner;
        [SerializeField] private SaucerSpawner _saucerSpawner;
        [Min(1)]
        [SerializeField] private int _startAsteroidCount;
        [Min(1)]
        [SerializeField] private int _deltaAsteroidsBetweenLevel;
        [SerializeField] private float _delayBetweenSpawnAsteroids;

        private int _currentAsteroidCount;
        private int _currentDestroedAsteriod = 0; 
        private WaitForSeconds _delay;

        private int _points = 0;

        public event System.Action<int> ChangedPoints;

        private void Start()
        {
            _asteroidPool.CreatedAsteroid += OnCreateAsteroid;

            _currentAsteroidCount = _startAsteroidCount;
            _delay = new WaitForSeconds(_delayBetweenSpawnAsteroids);
            _asteroidPool.Initialize();

            _saucerSpawner.SaucerDestroed += OnSaucerDestroed;

            ChangedPoints?.Invoke(_points);
            StartCoroutine(SpawnAsteroidWithDelay());
        }

        private void OnDisable()
        {
            _saucerSpawner.SaucerDestroed -= OnSaucerDestroed;
        }

        private void OnCreateAsteroid(Asteroid asteroid)
        {
            asteroid.PartDestroed += OnAsteroidPartDestroy;
            asteroid.FullDestroed += OnAsteroidFullDestroy;
        }

        private void OnSaucerDestroed(int points)
        {
            _points += points;
            ChangedPoints?.Invoke(_points);
        }

        private void OnAsteroidPartDestroy(int points)
        {
            _points += points;
            ChangedPoints?.Invoke(_points);
        }

        private void OnAsteroidFullDestroy(Asteroid asteroid)
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