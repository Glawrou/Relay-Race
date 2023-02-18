using UnityEngine;

namespace AndreyNosov.RelayRace.Game
{
    public class CursorSphere : MonoBehaviour
    {
        [SerializeField] private Connector _connectorPrefab;
        private MeshRenderer _renderer;
        
        private const float AcceptableHeight = 1;

        private static Point _currentPoint;
        private Point _startPoint;
        private Point _finishPoint;

        public void MoveMouseHandler(Vector3 input)
        {
            transform.position = new Vector3(input.x, AcceptableHeight, input.z);
        }

        public void MouseDownHandle(Vector3 input)
        {
            CheckRenderer();
            _renderer.material.color = Color.red;
            _startPoint = _currentPoint;
        }

        public void MouseUpHandle(InputMouseData inputMouseData)
        {
            CheckRenderer();
            _renderer.material.color = Color.white;
            _finishPoint = _currentPoint;

            if (_startPoint != null && _finishPoint != null && _startPoint != _finishPoint)
            {
                Instantiate(_connectorPrefab, null).Fill(_startPoint, _finishPoint);
            }
        }

        private void CheckRenderer()
        {
            if (_renderer == null)
            {
                _renderer = GetComponent<MeshRenderer>();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == Point.PointTag)
            {
                _currentPoint = other.gameObject.GetComponent<Point>();
                _currentPoint.Select();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == Point.PointTag && _currentPoint.gameObject == other.gameObject)
            {
                _currentPoint.Select();
                _currentPoint = null;
            }
        }
    }
}
