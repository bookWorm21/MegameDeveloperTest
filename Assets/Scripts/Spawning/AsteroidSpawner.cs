using Assets.Scripts.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Spawning
{
    public class AsteroidSpawner : MonoBehaviour
    {
        [SerializeField] private AsteroidPool _pool;
        [SerializeField] private float _minDistanceFromPlayer;
        [SerializeField] private Ship.Ship _ship;

        private float _maxDistanceFromPlayer;

        public void SpawnAsteroids(int count)
        {
            _maxDistanceFromPlayer = GameMap.MapWight;
            Vector3 shipPosition = _ship.transform.position;
            float deltaAngle = 360.0f / count;
            for (int i = 0; i < count; i++)
            {
                if(_pool.TryGetAsteroid(out Asteroid asteroid))
                {
                    float randomRadius = Random.Range(_minDistanceFromPlayer, _maxDistanceFromPlayer);
                    float angle = Random.Range(0, (i+1) * deltaAngle);
                    angle = angle / 180.0f * Mathf.PI;
                    Vector3 position = new Vector3(shipPosition.x + randomRadius * Mathf.Cos(angle),
                                                   shipPosition.y + randomRadius * Mathf.Sin(angle),
                                                   0);

                    Vector3 rotation = new Vector3(0, 0, Random.Range(0, 361));

                    position.x = Mathf.Clamp(position.x, GameMap.MinX, GameMap.MaxX);
                    position.y = Mathf.Clamp(position.y, GameMap.MinY, GameMap.MaxY);

                    asteroid.gameObject.transform.position = position;
                    asteroid.gameObject.transform.eulerAngles = rotation;
                    asteroid.gameObject.SetActive(true);
                    asteroid.Activate();
                }
            }
        }
    }
}