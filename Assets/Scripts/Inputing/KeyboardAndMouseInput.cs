using UnityEngine;

namespace Assets.Scripts.Inputing
{
    public class KeyboardAndMouseInput : ShipInputting
    {
        [SerializeField] private Ship.ShipMovable _ship;

        private float _needAngle;
        private Camera _mainCamera;
        private Vector3 _cursorPosition;
        private Vector3 _directionToCursor;

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            _cursorPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _directionToCursor = _cursorPosition - _ship.transform.position;
            _directionToCursor = _directionToCursor.normalized;
            _needAngle = Vector2.Angle(_ship.CurrentDirection, _directionToCursor) / 180.0f * Mathf.PI;
        
            if (Vector3.Cross(_directionToCursor, _ship.CurrentDirection).z < 0)
            {
                _needAngle *= -1.0f;
            }

            _rotation = Mathf.Clamp(_needAngle, -1, 1);

            _needShoot = Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);
            _needBoost = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetMouseButton(1);
        }
    }
}