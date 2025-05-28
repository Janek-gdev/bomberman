using Bomberman.Level;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bomberman.CameraControl
{
    /// <summary>
    /// Clamps the camera to within the level and handles player following
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class CameraClamp2D : MonoBehaviour
    {
        [SerializeField] private LevelLayoutGeneratorModel _levelLayoutGeneratorModel;
        [SerializeField] private Transform _target;
        [SerializeField] private float _uiCameraOffset;
        private Vector2 _min;
        private Vector2 _max;
        private Camera _camera;
        
        private float _orthographicSize;
        private float _aspect;
        private float _halfHeight;
        private float _halfWidth;

        private void Awake()
        {
            _min = _levelLayoutGeneratorModel.MinTilePosition;
            _max = _levelLayoutGeneratorModel.MaxTilePosition;
            _camera = GetComponent<Camera>();
            Assert.IsTrue(_camera.orthographic);

            _orthographicSize = _camera.orthographicSize;
            _aspect = _camera.aspect;
            _halfHeight = _orthographicSize;
            _halfWidth = _orthographicSize * _aspect;
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
            newCameraPosition.y = Mathf.Clamp(newCameraPosition.y, _min.y + _halfHeight, _uiCameraOffset + _max.y - _halfHeight);
            return newCameraPosition;
        }
    }
}