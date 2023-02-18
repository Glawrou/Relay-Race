using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AndreyNosov.RelayRace.Game.Storage
{
    public class Inventory
    {
        public List<InventoryItemType> Storage { get; private set; }

        public Action<InventoryItemType[]> OnChanges;

        public Inventory(InventoryItemType[] inventaryItems = null)
        {
            Storage = new List<InventoryItemType>();
            if (inventaryItems == null)
            {
                return;
            }

            Storage.AddRange(inventaryItems);
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
            Storage.Add(item);
            OnChanges?.Invoke(Storage.ToArray());
        }

        public bool Remove(InventoryItemType item)
        {
            if (!Storage.Any(i => i == item))
            {
                return false;
            }

            Storage.Remove(Storage.Find(i => i == item));
            OnChanges?.Invoke(Storage.ToArray());
            return true;
        }
    }
}
