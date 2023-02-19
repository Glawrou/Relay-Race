using UnityEngine;

namespace AndreyNosov.RelayRace.Game
{
    [RequireComponent(typeof(LineRenderer))]
    public class Connector : MonoBehaviour
    {
        [SerializeField] private Point _startPoint;
        [SerializeField] private Point _finishPoint;
        [SerializeField] private CursorSphere _cursorSphere;

        private LineRenderer _line;

        private void Awake()
        {
            _line = GetComponent<LineRenderer>();
        }

        public void Fill(Point startPoint, CursorSphere finishPoint)
        {
            _startPoint = startPoint;
            _cursorSphere = finishPoint;
        }

        public void Connect(Point startPoint, Point finishPoint)
        {
            _startPoint = startPoint;
            _finishPoint = finishPoint;
            _startPoint.Connect(_finishPoint);
        }

        private void LateUpdate()
        {   
            if (_startPoint == null || _cursorSphere == null)
            {
                Destroy(gameObject);
            }

            if (_finishPoint == null)
            {
                ConnectPoints(_startPoint.transform, _cursorSphere.transform);
                return;
            }

            ConnectPoints(_startPoint.transform, _finishPoint.transform);
        }

        private void ConnectPoints(Transform point1, Transform point2)
        {
            if (point1 == null || point2 == null)
            {
                _line.enabled = false;
                return;
            }

            _line.enabled = true;
            _line.SetPositions(new Vector3[] { point1.position, point2.position });
        }
    }
}
