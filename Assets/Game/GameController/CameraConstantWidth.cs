using UnityEngine;

namespace AndreyNosov.RelayRace.Game
{
    [ExecuteAlways]
    public class CameraConstantWidth : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Vector2 _targetAspect = new Vector2(16, 9);
        [SerializeField] private float _minSize = 5;

        private void Update()
        {
            if (!_camera)
            {
                return;
            }

            var aspectRatio = _targetAspect.x / _targetAspect.y;
            var correctedSize = _minSize * (aspectRatio / _camera.aspect);
            _camera.orthographicSize = _camera.aspect > aspectRatio ? _minSize : correctedSize;
        }
    }
}