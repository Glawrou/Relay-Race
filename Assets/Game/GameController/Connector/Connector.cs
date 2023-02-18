using UnityEngine;

namespace AndreyNosov.RelayRace.Game
{
    [RequireComponent(typeof(LineRenderer))]
    public class Connector : MonoBehaviour
    {
        [SerializeField] private Point _startPoint;
        [SerializeField] private Point _finishPoint;

        private LineRenderer _line;

        private void Awake()
        {
            _line = GetComponent<LineRenderer>();
        }

        public void Connect(Point startPoint, Point finishPoint)
        {
            _startPoint = startPoint;
            _finishPoint = finishPoint;
            _startPoint.Connect(_finishPoint);
        }

        private void LateUpdate()
        {
            if (_startPoint == null || _finishPoint == null)
            {
                _line.enabled = false;
                return;
            }

            _line.enabled = true;
            _line.SetPositions(new Vector3[] { _startPoint.transform.position, _finishPoint.transform.position });
        }
    }
}
