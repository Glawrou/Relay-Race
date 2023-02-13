using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
            if (_runers.Count > 0)
            {
                var site = FindFreeSeat();
                site.PlaceOwner = _runers.Dequeue();
                site.PlaceOwner.SetSite(site);
            }
        }

        private Site FindFreeSeat()
        {
            return _sites.FirstOrDefault(s => s.PlaceOwner == null);
        }
    }
}
