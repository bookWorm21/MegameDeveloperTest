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

        private Asteroid[] _asteroids;

        public int GetAsteroidsSize => _asteroids.Length;

        public Asteroid GetAsteroid(int index)
        {
            if(index < 0 && index >= _asteroids.Length)
            {
                Debug.LogError("Попытка передать индекс, который выходит за пределы массива");
                return null; 
            }

            return _asteroids[index];
        }

        public void Initialize()
        {
            _asteroids = new Asteroid[_capacity];

            for(int i = 0; i < _capacity; i++)
            {
                Asteroid big = Instantiate(_bigAsteroidPrefab, _containerForBig);
                _asteroids[i] = big;
                Asteroid[] bigChilds = new Asteroid[big.ChildCount];

                for (int j = 0; j < big.ChildCount; j++)
                {
                    Asteroid medium = Instantiate(_mediumAsteroidPrefab, _containterForMedium);
                    bigChilds[j] = medium;
                    Asteroid[] mediumChilds = new Asteroid[medium.ChildCount];

                    for(int k = 0; k < medium.ChildCount; k++)
                    {
                        mediumChilds[k] = Instantiate(_smallAsteroidPrefab, _containerForSmall);
                        mediumChilds[k].gameObject.SetActive(false);
                    }

                    medium.gameObject.SetActive(false);
                    medium.SetChilds(mediumChilds);
                }

                big.gameObject.SetActive(false);
                big.SetChilds(bigChilds);
            }
        }

        public bool TryGetAsteroid(out Asteroid asteroid)
        {
            asteroid = _asteroids.FirstOrDefault(p => p.IsActive == false);
            return asteroid != null;
        }
    }
}