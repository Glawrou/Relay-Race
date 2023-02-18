using System.Collections;
using UnityEngine;
using AndreyNosov.RelayRace.Game.Storage;

namespace AndreyNosov.RelayRace.Game
{
    public class Runer : MonoBehaviour
    {
        public Inventory Inventary { get; private set; }

        public const string RunnerTag = "Runer";

        [SerializeField] private float _speed;
        [SerializeField] private InventoryDisplay _inventoryDisplay;

        private const float ConnectionDistance = 0.1f;
        private const int PointsPerRadius = 20;

        private Coroutine _go;

        private void Awake()
        {
            Inventary = new Inventory();
            Inventary.OnChanges += _inventoryDisplay.UpdateState;
        }

        public void Go(Vector3[] points)
        {
            ClearDirections();
            _go = StartCoroutine(MovementProcess(points));
        }

        public void GoOrbit(Vector3 points, float radius)
        {
            var numPoints = (int)radius * PointsPerRadius;
            ClearDirections();
            _go = StartCoroutine(MovementOrbitProcess(GetOrbit(points, radius, numPoints)));
        }

        public Inventory Connect()
        {
            return Inventary;
        }

        private IEnumerator MovementOrbitProcess(Vector3[] points)
        {
            while (true)
            {
                yield return MovementProcess(points);
            }
        }

        private IEnumerator MovementProcess(Vector3[] points)
        {
            foreach (var point in points)
            {
                transform.LookAt(point);
                while (Vector3.Distance(transform.position, point) > ConnectionDistance)
                {
                    transform.position = Vector3.MoveTowards(transform.position, point, _speed);
                    yield return new WaitForFixedUpdate();
                }
            }
        }

        private void ClearDirections()
        {
            if (_go == null)
            {
                return;
            }

            StopCoroutine(_go);
        }

        private Vector3[] GetOrbit(Vector3 point, float radius, int numPoints)
        {
            var orbitPoints = new Vector3[numPoints];
            var angleIncrement = 360f / numPoints;

            for (var i = 0; i < numPoints; i++)
            {
                var angle = i * angleIncrement;
                var rotation = Quaternion.Euler(0f, angle, 0f);
                var orbitPosition = point + rotation * (Vector3.right * radius);
                orbitPoints[i] = orbitPosition;
            }

            return orbitPoints;
        }
    }
}
