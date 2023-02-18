using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using AndreyNosov.RelayRace.Game.Storage;

namespace AndreyNosov.RelayRace.Game
{
    public class Point : MonoBehaviour
    {
        public Inventory Inventory { get; private set; }

        public const string PointTag = "Base";

        [SerializeField] private Site[] _sites;
        [SerializeField] private InventoryDisplay _inventoryDisplay;
        [SerializeField] private Runer _runerPrefab;

        private bool _isSelected = false;
        private Renderer _renderer;
        private Queue<Runer> _runers = new Queue<Runer>();

        private const float OrbitalRadius = 6f;

        private void Awake()
        {
            Inventory = new Inventory();
            _renderer = GetComponent<Renderer>();
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

        public void Select()
        {
            _isSelected = !_isSelected;
            _renderer.material.color = _isSelected ? Color.red : Color.white;
        }

        public void AddQueue(Runer runer)
        {
            _runers.Enqueue(runer);
        }

        public void Connect(Point point)
        {
            if (!_sites.Any(s => s.PlaceOwner != null))
            {
                return;
            }

            var runer = _sites.FirstOrDefault(s => s.PlaceOwner != null).PlaceOwner;
            runer.Go(point.transform.position);
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
