using System.Collections;
using UnityEngine;

namespace AndreyNosov.RelayRace.Game
{
    public class Runer : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Site _currentSite;

        private const float ConnectionDistance = 0.1f;

        public void SetSite(Site site)
        {
            _currentSite = site;
            StartCoroutine(GoTiSite());
        }

        private IEnumerator GoTiSite()
        {
            var nearPoint = _currentSite.GetNearPoint();
            var farPoint = _currentSite.GetFarPoint();

            transform.LookAt(farPoint);
            yield return new WaitForSeconds(1);

            while (Vector3.Distance(transform.position, farPoint) > ConnectionDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, farPoint, _speed);
                yield return new WaitForFixedUpdate();
                transform.LookAt(farPoint);
            }

            transform.LookAt(nearPoint);
            yield return new WaitForSeconds(1);

            while (Vector3.Distance(transform.position, nearPoint) > ConnectionDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, nearPoint, _speed);
                yield return new WaitForFixedUpdate();
                transform.LookAt(nearPoint);
            }
        }
    }
}
