using AndreyNosov.RelayRace.Game.Storage;
using UnityEngine;

namespace AndreyNosov.RelayRace.Game
{
    public class PointFiller : MonoBehaviour
    {
        [SerializeField] private InventoryItemType[] _inventoryItems;
        [SerializeField] private Point _point;

        private void Start()
        {
            foreach (var item in _inventoryItems)
            {
                _point.MyInventory.Add(item);
            }
        }
    }
}
