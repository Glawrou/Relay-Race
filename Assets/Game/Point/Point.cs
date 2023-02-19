using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using AndreyNosov.RelayRace.Game.Storage;
using System;

namespace AndreyNosov.RelayRace.Game
{
    public class Point : MonoBehaviour
    {
        public Action<Point> OnReceived;

        public Inventory MyInventory { get; private set; }

        public const string PointTag = "Base";

        [SerializeField] private Site[] _sites;
        [SerializeField] private InventoryDisplay _inventoryDisplay;
        [SerializeField] private Runer _runerPrefab;
        [SerializeField] private bool _isNeedItem = false;
        [SerializeField] private InventoryItemType _needItemType;

        private bool _isSelected = false;
        private Renderer _renderer;
        private Color _defaultColot;
        private Queue<Runer> _runers = new Queue<Runer>();

        public const float OrbitalRadius = 6f;

        private void Awake()
        {
            MyInventory = new Inventory();
            _renderer = GetComponent<Renderer>();
            foreach (var site in _sites)
            {
                site.OnHandedOver += HandOverHandler;
            }

            MyInventory.OnChanges += _inventoryDisplay.UpdateState;
            _defaultColot = GetColor(_needItemType);
            _renderer.material.color = _defaultColot;
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
            _renderer.material.color = _isSelected ? Color.red : _defaultColot;
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
            if (MyInventory.Storage.Count != 0)
            {
                runer.Inventary.Transfer(MyInventory, MyInventory.Storage[0]);
            }
            
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
                site.PlaceOwner.GoToSite(site.GetPathToSite(), transform.position);
            }
        }

        private Color GetColor(InventoryItemType itemType)
        {
            if (!_isNeedItem)
            {
                return Color.white;
            }

            switch (itemType)
            {
                case InventoryItemType.RelayBatonBlue:
                    return Color.blue;
                case InventoryItemType.RelayBatonRed:
                    return Color.red;
                case InventoryItemType.RelayBatonYellow:
                    return Color.yellow;
                case InventoryItemType.RelayBatonViolet:
                    return Color.cyan;
                default:
                    return Color.white;
            }
        }

        private Site FindFreeSeat()
        {
            return _sites.FirstOrDefault(s => s.PlaceOwner == null);
        }

        private void HandOverHandler(Inventory inventory)
        {
            if (_isNeedItem && inventory.Storage.Any(s => s == _needItemType))
            {
                inventory.Remove(_needItemType);
                _renderer.material.color = Color.white;
                _defaultColot = Color.white;
                _isNeedItem = false;
                OnReceived?.Invoke(this);
            }

            var items = inventory.Storage.ToArray();
            foreach (var item in items)
            {
                MyInventory.Transfer(inventory, item);
            }
        }
    }
}
