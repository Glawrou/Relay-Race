using UnityEngine;

namespace AndreyNosov.RelayRace.Game.Storage
{
    public class InventoryDisplay : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        [SerializeField] private InventoryItemView _itemViewPrefab;
        [SerializeField] private Transform _panel;

        private InventoryItemView[] _items;

        private void Awake()
        {
            if (_camera == null)
            {
                _camera = Camera.main;
                Debug.LogWarning("InventoryDisplay > Awake > (The link to the camera was empty. I had to look for a camera.)");
            }
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

        private void LateUpdate()
        {
            transform.LookAt(_camera.transform.position);
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
