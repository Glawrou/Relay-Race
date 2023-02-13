using UnityEngine;

namespace AndreyNosov.RelayRace.Game
{
    public class DetectionField : MonoBehaviour
    {
        [SerializeField] private Point _point;

        private const string TagRuner = "Runer";

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == TagRuner)
            {
                _point.AddQueue(other.gameObject.GetComponent<Runer>());
            }
        }
    }
}
