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

        private Connector _connector;

        public void MoveMouseHandler(Vector3 input)
        {
            transform.position = new Vector3(input.x, AcceptableHeight, input.z);
        }

        public void MouseDownHandle(Vector3 input)
        {
            CheckRenderer();
            _renderer.material.color = Color.red;
            if (_currentPoint == null)
            {
                return;
            }

            DestroyLine();
            _startPoint = _currentPoint;
            _connector = Instantiate(_connectorPrefab, null);
            _connector.Fill(_startPoint, this);
        }

        public void MouseUpHandle(InputMouseData inputMouseData)
        {
            CheckRenderer();
            _renderer.material.color = Color.white;
            _finishPoint = _currentPoint;
            DestroyLine();

            if (_startPoint != null && _finishPoint != null && _startPoint != _finishPoint)
            {
                _connector = Instantiate(_connectorPrefab, null);
                _connector.Connect(_startPoint, _finishPoint);
            }
        }

        private void CheckRenderer()
        {
            if (_renderer == null)
            {
                _renderer = GetComponent<MeshRenderer>();
            }
        }

        private void DestroyLine()
        {
            if (_connector)
            {
                Destroy(_connector.gameObject);
                _connector = null;
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
