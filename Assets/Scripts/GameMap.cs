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

        public static float MaxWight { get; private set; }
        public static float MinWight { get; private set; }
        public static float MaxHeight { get; private set; }
        public static float MinHeight { get; private set; }

        private void Start()
        {
            _camera = Camera.main;
            _collider = GetComponent<BoxCollider>();

            Vector3 leftBottomPoint = _camera.ScreenToWorldPoint(new Vector3(0, 0, 0));
            Vector3 rightBottomPoint = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

            MaxWight = rightBottomPoint.x;
            MinWight = leftBottomPoint.x;

            MaxHeight = rightBottomPoint.y;
            MinHeight = leftBottomPoint.y;

            MapWight = MaxWight - MinWight;
            MapHeight = MaxHeight - MinHeight;

            _collider.size = new Vector3(
                (rightBottomPoint.x - leftBottomPoint.x),
                (rightBottomPoint.y - leftBottomPoint.y),
                1);
        }

        private void OnTriggerExit(Collider other)
        {
            Vector3 position = other.transform.position;

            if(position.x >= MaxWight)
            {
                position.x = MinWight + 0.1f;
            }
            else if(position.x <= MinWight)
            {
                position.x = MaxWight - 0.1f;
            }

            if(position.y >= MaxHeight)
            {
                position.y = MinHeight + 0.1f;
            }
            else if(position.y <= MinHeight)
            {
                position.y = MaxHeight - 0.1f;
            }

            other.transform.position = position;
        }
    }
}