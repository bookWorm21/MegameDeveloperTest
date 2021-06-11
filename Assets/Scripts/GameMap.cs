using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(BoxCollider))]
    public class GameMap : MonoBehaviour
    {
        public static float MapWight;
        public static float MapHeight;

        private BoxCollider _collider;
        private Camera _camera;

        public static float MaxX { get; private set; }
        public static float MinX { get; private set; }
        public static float MaxY { get; private set; }
        public static float MinY { get; private set; }

        private void Start()
        {
            _camera = Camera.main;
            _collider = GetComponent<BoxCollider>();

            Vector3 leftBottomPoint = _camera.ScreenToWorldPoint(new Vector3(0, 0, 0));
            Vector3 rightBottomPoint = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

            MaxX = rightBottomPoint.x;
            MinX = leftBottomPoint.x;

            MaxY = rightBottomPoint.y;
            MinY = leftBottomPoint.y;

            MapWight = MaxX - MinX;
            MapHeight = MaxY - MinY;

            _collider.size = new Vector3(
                (rightBottomPoint.x - leftBottomPoint.x),
                (rightBottomPoint.y - leftBottomPoint.y),
                1);
        }

        private void OnTriggerExit(Collider other)
        {
            Vector3 position = other.transform.position;

            if(position.x >= MaxX)
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

            other.transform.position = position;
        }
    }
}