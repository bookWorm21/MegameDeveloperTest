using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameMap : MonoBehaviour
    {
        [SerializeField] private MapBorder _leftBorder;
        [SerializeField] private MapBorder _rightBorder;
        [SerializeField] private MapBorder _upBorder;
        [SerializeField] private MapBorder _downBorder;
        [SerializeField] private float _borderWight;

        public static float MapWight;
        public static float MapHeight;

        private Camera _camera;

        public static float MaxX { get; private set; }
        public static float MinX { get; private set; }
        public static float MaxY { get; private set; }
        public static float MinY { get; private set; }

        private void OnEnable()
        {
            _leftBorder.Triggered += OnObjectOutsideMap;
            _rightBorder.Triggered += OnObjectOutsideMap;
            _upBorder.Triggered += OnObjectOutsideMap;
            _downBorder.Triggered += OnObjectOutsideMap;
        }

        private void Awake()
        { 
            _camera = Camera.main;

             Vector3 leftBottomPoint = _camera.ScreenToWorldPoint(new Vector3(0, 0, 0));
            Vector3 rightBottomPoint = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

            MaxX = rightBottomPoint.x;
            MinX = leftBottomPoint.x;

            MaxY = rightBottomPoint.y;
            MinY = leftBottomPoint.y;

            MapWight = MaxX - MinX;
            MapHeight = MaxY - MinY;

            _leftBorder.transform.localScale = new Vector3(_borderWight, MapHeight, 1);
            _leftBorder.transform.position = new Vector3(MinX - _borderWight / 2, 1, 0);

            _rightBorder.transform.localScale = _leftBorder.transform.localScale;
            _rightBorder.transform.position = new Vector3(MaxX + _borderWight / 2, 1, 0);

            _upBorder.transform.localScale = new Vector3(MapWight, _borderWight, 1);
            _upBorder.transform.position = new Vector3(0, MaxY + _borderWight / 2, 0);

            _downBorder.transform.localScale = _upBorder.transform.localScale;
            _downBorder.transform.position = new Vector3(0, MinY - _borderWight / 2, 0);
        }

        private void OnDisable()
        {
            _leftBorder.Triggered -= OnObjectOutsideMap;
            _rightBorder.Triggered -= OnObjectOutsideMap;
            _upBorder.Triggered -= OnObjectOutsideMap;
            _downBorder.Triggered -= OnObjectOutsideMap;
        }

        private void OnObjectOutsideMap(Transform some)
        {
            Transform current = some;

            if (some.TryGetComponent(out Enemies.EnemiesHealth asteroid))
            {
                current = asteroid.Main;
            }
            

            Vector3 position = current.position;

            if (position.x >= MaxX)
            {
                position.x = MinX + 0.1f;
            }
            else if(position.x <= MinX)
            {
                position.x = MaxX - 0.1f;
            }

            if(position.y >= MaxY)
            {
                position.y = MinY + 0.1f;
            }
            else if(position.y <= MinY)
            {
                position.y = MaxY - 0.1f;
            }

            current.position = position;
        }
    }
}