using System.Collections;
using UnityEngine;
using AndreyNosov.RelayRace.Game.Storage;
using System.Collections.Generic;
using System;
using System.Linq;

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

        public void Go(Vector3 point)
        {
            Go(new Vector3[] { point });
        }
        
        public void GoToSite(Vector3[] site, Vector3 point)
        {
            ClearDirections();
            _go = StartCoroutine(MovementProcess(CalculateTrajectory(site, point)));
        }

        public void GoExitSite(Vector3 newPoint, Vector3 point)
        {
            ClearDirections();
            _go = StartCoroutine(MovementProcess(CalculateTrajectory(new Vector3[] { newPoint }, point)));
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

        private Vector3[] CalculateTrajectory(Vector3[] site, Vector3 orbitPoint)
        {
            var radius = Point.OrbitalRadius;
            var numPoints = (int)radius * PointsPerRadius;
            var orbit = GetOrbit(orbitPoint, radius, numPoints);
            var entrance = FindClosestOrbitPoint(orbit, transform.position);
            var exit = FindClosestOrbitPoint(orbit, site[0]);
            var list = new List<Vector3>();
            list.AddRange(GetPointsBetweenEntranceAndExit(orbit, entrance, exit));
            list.AddRange(site);
            return list.ToArray();
        }

        private static Vector3 FindClosestOrbitPoint(Vector3[] orbit, Vector3 point)
        {
            var closestPoint = Vector3.zero;
            var closestDistance = Mathf.Infinity;

            for (var i = 0; i < orbit.Length; i++)
            {
                var distance = Vector3.Distance(orbit[i], point);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPoint = orbit[i];
                }
            }

            return closestPoint;
        }

        public static Vector3[] GetPointsBetweenEntranceAndExit(Vector3[] orbit, Vector3 entrance, Vector3 exit)
        {
            var entranceIndex = Array.IndexOf(orbit, orbit.OrderBy(p => Vector3.Distance(p, entrance)).First());
            var exitIndex = Array.IndexOf(orbit, orbit.OrderBy(p => Vector3.Distance(p, exit)).First());

            if (entranceIndex > exitIndex)
            {
                (entranceIndex, exitIndex) = (exitIndex, entranceIndex);
            }

            return orbit.Skip(entranceIndex).Take(exitIndex - entranceIndex + 1).ToArray();
        }
    }
}
