using System.Collections;
using UnityEngine;

namespace AndreyNosov.RelayRace.Game
{
    public class Runer : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private const float ConnectionDistance = 0.1f;
        private const float DelayBetweenPoints = 1f;

        private Coroutine _go;

        public void Go(Vector3[] points)
        {
            ClearDirections();
            _go = StartCoroutine(MovementProcess(points));
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

                yield return new WaitForSeconds(DelayBetweenPoints);
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
    }
}
