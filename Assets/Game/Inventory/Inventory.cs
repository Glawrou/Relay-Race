using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AndreyNosov.RelayRace.Game.Storage
{
    public class Inventory
    {
        public Action<InventoryItemType[]> OnChanges;

        private List<InventoryItemType> _inventary;

        public Inventory(InventoryItemType[] inventaryItems = null)
        {
            _inventary = new List<InventoryItemType>();
            if (inventaryItems == null)
            {
                return;
            }

            _inventary.AddRange(inventaryItems);
        }

        public void Transfer(Inventory invenraty, InventoryItemType item)
        {
            if (!invenraty.Remove(item))
            {
                Debug.LogWarning("Inventary > Transfer > (There is no transfer item in inventory)");
                return;
            }

            Add(item);
        }

        public void Add(InventoryItemType item)
        {
            _inventary.Add(item);
            OnChanges?.Invoke(_inventary.ToArray());
        }

        public bool Remove(InventoryItemType item)
        {
            if (!_inventary.Any(i => i == item))
            {
                return false;
            }

            _inventary.Remove(_inventary.Find(i => i == item));
            OnChanges?.Invoke(_inventary.ToArray());
            return true;
        }
    }
}
