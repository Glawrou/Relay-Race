using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AndreyNosov.RelayRace.Game
{
    public class Point : MonoBehaviour
    {
        [SerializeField] private Site[] _sites;

        private Queue<Runer> _runers = new Queue<Runer>();

        public void AddQueue(Runer runer)
        {
            _runers.Enqueue(runer);
        }

        private void FixedUpdate()
        {
            FillPlaces();
        }

        private void FillPlaces()
        {
            if (!_sites.Any(s => s.PlaceOwner == null))
            {
                return;
            }

            if (_runers.Count > 0)
            {
                var site = FindFreeSeat();
                site.PlaceOwner = _runers.Dequeue();
                site.PlaceOwner.Go(site.GetPathToSite());
            }
        }

        private Site FindFreeSeat()
        {
            return _sites.FirstOrDefault(s => s.PlaceOwner == null);
        }
    }
}
