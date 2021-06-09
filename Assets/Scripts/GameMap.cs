using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(BoxCollider))]
    public class GameMap : MonoBehaviour
    {
        private BoxCollider _collider;
        private Camera _camera;

        private float _maxWight;
        private float _minWight;
        private float _maxHeight;
        private float _minHeight;

        private void Start()
        {
            _camera = Camera.main;
            _collider = GetComponent<BoxCollider>();

            Vector3 leftBottomPoint = _camera.ScreenToWorldPoint(new Vector3(0, 0, 0));
            Vector3 rightBottomPoint = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

            _maxWight = rightBottomPoint.x;
            _minWight = leftBottomPoint.x;

            _maxHeight = rightBottomPoint.y;
            _minHeight = leftBottomPoint.y;

            _collider.size = new Vector3(
                (rightBottomPoint.x - leftBottomPoint.x),
                (rightBottomPoint.y - leftBottomPoint.y),
                1);
        }

        private void OnTriggerExit(Collider other)
        {
            Vector3 position = other.transform.position;

            if(position.x >= _maxWight)
            {
                position.x = _minWight + 0.1f;
            }
            else if(position.x <= _minWight)
            {
                position.x = _maxWight - 0.1f;
            }

            if(position.y >= _maxHeight)
            {
                position.y = _minHeight + 0.1f;
            }
            else if(position.y <= _minHeight)
            {
                position.y = _maxHeight - 0.1f;
            }

            other.transform.position = position;
        }
    }
}