using RpgLogic.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RpgLogic.Npc.Vendors
{
    public class VendorItemButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI itemNameText = null;
        [SerializeField] private Image itemIconImage = null;

        private VendorSystem vendorSystem = null;
        private ItemRPG item = null;

        public void Initialize(VendorSystem vendorSystem, ItemRPG item, int quantity)
        {
            this.vendorSystem = vendorSystem;
            this.item = item;

            string currentQuantity = quantity > 1 ? $"({quantity})" : "";

            itemNameText.text = $"{item.name} {currentQuantity} ";
            itemIconImage.sprite = item.Icon;
        }

        public void SelectItem()
        {
            vendorSystem.SetItem(item);
        }
    }
}