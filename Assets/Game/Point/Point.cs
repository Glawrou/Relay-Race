using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using AndreyNosov.RelayRace.Game.Storage;

namespace AndreyNosov.RelayRace.Game
{
    public class Point : MonoBehaviour
    {
        public Inventory Inventory { get; private set; }

        [SerializeField] private Site[] _sites;
        [SerializeField] private InventoryDisplay _inventoryDisplay;
        [SerializeField] private Runer _runerPrefab;

        private Queue<Runer> _runers = new Queue<Runer>();

        private const float OrbitalRadius = 6f;

        private void Awake()
        {
            Inventory = new Inventory();
            foreach (var site in _sites)
            {
                site.OnHandedOver += HandOverHandler;
            }

            Inventory.OnChanges += _inventoryDisplay.UpdateState;
        }

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            if (!_sites.Any(s => s.PlaceOwner == null))
            {
                Instantiate(_runerPrefab, _sites[0].GetPathToSite()[0], Quaternion.identity, null).GoOrbit(transform.position, OrbitalRadius);
                return;
            }

            var sites = _sites.FirstOrDefault(s => s.PlaceOwner == null);
            var runner = Instantiate(_runerPrefab, sites.GetPathToSite()[0], Quaternion.identity, null);
            runner.Go(sites.GetPathToSite());
        }

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

        private void HandOverHandler(Inventory inventory)
        {
            foreach (var item in inventory.Storage)
            {
                Inventory.Transfer(inventory, item);
            }
        }
    }
}
