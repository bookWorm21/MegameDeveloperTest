using Assets.Scripts.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Assets.Scripts.Spawning
{
    public class AsteroidPool : MonoBehaviour
    {
        [SerializeField] private Transform _containerForBig;
        [SerializeField] private Transform _containterForMedium;
        [SerializeField] private Transform _containerForSmall;

        [SerializeField] private Asteroid _bigAsteroidPrefab;
        [SerializeField] private Asteroid _mediumAsteroidPrefab;
        [SerializeField] private Asteroid _smallAsteroidPrefab;
        [SerializeField] private int _capacity;

        private Queue<Asteroid> _asteroidQueue = new Queue<Asteroid>();

        public event System.Action<Asteroid> CreatedAsteroid;

        public void Initialize()
        {
            for(int i = 0; i < _capacity; i++)
            {
                Asteroid current = InitAsteroid();
                current.FullDestroed += OnAsteroidDestroed;
                CreatedAsteroid?.Invoke(current);
                _asteroidQueue.Enqueue(current);
            }
        }

        public bool TryGetAsteroid(out Asteroid asteroid)
        {
            if (_asteroidQueue.Count > 0)
            {
                asteroid = _asteroidQueue.Dequeue();
            }
            else
            {
                asteroid = InitAsteroid();
                asteroid.FullDestroed += OnAsteroidDestroed;
                CreatedAsteroid?.Invoke(asteroid);
            }

            return asteroid != null;
        }

        private void OnAsteroidDestroed(Asteroid asteroid)
        {
            _asteroidQueue.Enqueue(asteroid);
        }

        private Asteroid InitAsteroid()
        {
            Asteroid big = Instantiate(_bigAsteroidPrefab, _containerForBig);
            Asteroid[] bigChilds = new Asteroid[big.ChildCount];

            for (int j = 0; j < big.ChildCount; j++)
            {
                Asteroid medium = Instantiate(_mediumAsteroidPrefab, _containterForMedium);
                bigChilds[j] = medium;
                Asteroid[] mediumChilds = new Asteroid[medium.ChildCount];

                for (int k = 0; k < medium.ChildCount; k++)
                {
                    mediumChilds[k] = Instantiate(_smallAsteroidPrefab, _containerForSmall);
                    mediumChilds[k].gameObject.SetActive(false);
                }

                medium.gameObject.SetActive(false);
                medium.SetChilds(mediumChilds);
            }

            big.gameObject.SetActive(false);
            big.SetChilds(bigChilds);
            
            return big;
        }
    }
}