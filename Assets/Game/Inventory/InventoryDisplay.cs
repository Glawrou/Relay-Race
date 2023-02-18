using UnityEngine;

namespace AndreyNosov.RelayRace.Game.Storage
{
    public class InventoryDisplay : MonoBehaviour
    {
        [SerializeField] private InventoryItemView _itemViewPrefab;
        [SerializeField] private Transform _panel;

        private InventoryItemView[] _items;

        private void Awake()
        {
            transform.LookAt(transform.position + Vector3.up);
        }

        public void UpdateState(InventoryItemType[] inventaryItems)
        {
            ClearItems();
            if (inventaryItems == null || inventaryItems.Length == 0)
            {
                _panel.gameObject.SetActive(false);
                return;
            }

            _panel.gameObject.SetActive(true);
            var itemsCount = inventaryItems.Length;
            _items = new InventoryItemView[itemsCount];
            for (var i = 0; i < itemsCount; i++)
            {
                _items[i] = Instantiate(_itemViewPrefab, _panel);
                _items[i].Fill(inventaryItems[i]);
            }
        }

        private void ClearItems()
        {
            if (_items == null)
            {
                return;
            }

            for (var i = 0; i < _items.Length; i++)
            {
                Destroy(_items[i].gameObject);
            }

            _items = null;
        }
    }
}
