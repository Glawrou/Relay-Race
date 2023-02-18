using UnityEngine;
using UnityEngine.UI;

namespace AndreyNosov.RelayRace.Game.Storage
{
    public class InventoryItemView : MonoBehaviour
    {
        public InventoryItemType InventaryItemType { get; private set; }

        [SerializeField] private Image _myImage;

        public void Fill(InventoryItemType inventaryItem)
        {
            InventaryItemType = inventaryItem;
            _myImage.sprite = InventorySpriteGether.GetSprite(InventaryItemType);
        }
    }
}
