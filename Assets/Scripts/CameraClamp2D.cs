using Bomberman.Level;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bomberman.CameraControl
{
    [RequireComponent(typeof(Camera))]
    public class CameraClamp2D : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        private Vector2 _min;
        private Vector2 _max;
        private Camera _camera;
        
        private float _orthoSize;
        private float _aspect;
        private float _halfHeight;
        private float _halfWidth;

        private void Awake()
        {
            _min = LevelLayoutGeneratorModel.instance.MinTilePosition;
            _max = LevelLayoutGeneratorModel.instance.MaxTilePosition;
            _camera = GetComponent<Camera>();
            Assert.IsTrue(_camera.orthographic);

            _orthoSize = _camera.orthographicSize;
            _aspect = _camera.aspect;
            _halfHeight = _orthoSize;
            _halfWidth = _orthoSize * _aspect;
        }

        private void LateUpdate()
        {
            var newCameraPosition = TrackTarget();

            newCameraPosition = ClampCamera(newCameraPosition);

            transform.position = new Vector3(newCameraPosition.x, newCameraPosition.y, transform.position.z);
        }

        private Vector2 TrackTarget()
        {
            var targetPosition = _target.position;
            var newCameraPosition = new Vector2(targetPosition.x, targetPosition.y);

            return newCameraPosition;
        }

        private Vector2 ClampCamera(Vector2 newCameraPosition)
        {
            newCameraPosition.x = Mathf.Clamp(newCameraPosition.x, _min.x + _halfWidth, _max.x - _halfWidth);
            newCameraPosition.y = Mathf.Clamp(newCameraPosition.y, _min.y + _halfHeight, _max.y - _halfHeight);
            return newCameraPosition;
        }
    }
}