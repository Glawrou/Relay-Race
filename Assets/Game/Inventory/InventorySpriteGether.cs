using System.Collections.Generic;
using UnityEngine;

namespace AndreyNosov.RelayRace.Game.Storage
{
    public static class InventorySpriteGether
    {
        private static Dictionary<InventoryItemType, string> _spritePath;

        private const string PathItems = "ItemSprites/";

        public static Sprite GetSprite(InventoryItemType inventoryItem)
        {
            return Resources.Load<Sprite>(GetPath(inventoryItem));
        }

        private static string GetPath(InventoryItemType inventoryItem)
        {
            GetPaths().TryGetValue(inventoryItem, out var path);
            return path;
        }

        private static Dictionary<InventoryItemType, string> GetPaths()
        {
            if (_spritePath != null)
            {
                return _spritePath;
            }

            _spritePath = new Dictionary<InventoryItemType, string>()
            {
                { InventoryItemType.RelayBatonBlue, PathItems + "Blue"},
                { InventoryItemType.RelayBatonRed, PathItems + "Red"},
                { InventoryItemType.RelayBatonViolet, PathItems + "Violet"},
                { InventoryItemType.RelayBatonYellow, PathItems + "Yellow"}
            };

            return _spritePath;
        }
    }
}
