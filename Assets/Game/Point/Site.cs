using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AndreyNosov.RelayRace.Game
{
    public class Site : MonoBehaviour
    {
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

        public Vector3 GetFarPoint()
        {
            return _farPoint.transform.position;
        }

        public Vector3 GetNearPoint()
        {
            return _nearPoint.transform.position;
        }
    }
}
