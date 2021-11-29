using UnityEngine;

namespace Src.Controllers
{
    public class CameraController
    {
        private float _x;
        private float _y;

        private float _offsetX;
        private float _offsetY = 1.5f;

        private int _cameraSpeed = 300;

        private readonly Transform _playerTransform;
        private readonly Transform _cameraTransform;
        private readonly Transform _background;

        public CameraController(Transform playerTransform, Transform cameraTransform, Transform background)
        {
            _playerTransform = playerTransform;
            _cameraTransform = cameraTransform;
            _background = background;
        }

        public void Update()
        {
            _x = _playerTransform.transform.position.x;
            _y = _playerTransform.transform.position.y;

            _cameraTransform.transform.position = Vector3.Lerp(
                _cameraTransform.position,
                new Vector3(_x + _offsetX, _y + _offsetY, _cameraTransform.position.z),
                Time.deltaTime * _cameraSpeed
            );
            _background.transform.position = _playerTransform.transform.position;
        }
    }
}