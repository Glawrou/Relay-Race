using AndreyNosov.RelayRace.Game.Storage;
using System;
using UnityEngine;

namespace AndreyNosov.RelayRace.Game
{
    public class Site : MonoBehaviour
    {
        public Action<Inventory> OnHandedOver;

        [SerializeField] private Transform _farPoint;
        [SerializeField] private Transform _nearPoint;

        public Runer PlaceOwner
        {
            get
            {
                return _placeOwner;
            }
            set
            {
                _placeOwner = value;
            }
        }

        private Runer _placeOwner;

        public Vector3[] GetPathToSite()
        {
            return new Vector3[] { _farPoint.transform.position, _nearPoint.transform.position };
        }

        private void HandOver(Inventory inventory)
        {
            if (inventory.Storage != null && inventory.Storage.Count != 0)
            {
                OnHandedOver.Invoke(inventory);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (PlaceOwner != null)
            {
                return;
            }

            if (other.tag == Runer.RunnerTag)
            {
                PlaceOwner = other.GetComponent<Runer>();
                HandOver(PlaceOwner.Connect());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == Runer.RunnerTag && PlaceOwner == other.GetComponent<Runer>())
            {
                PlaceOwner = null;
            }
        }
    }
}
