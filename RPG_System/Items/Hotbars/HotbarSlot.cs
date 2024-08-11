using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RpgLogic.Items.Hotbars
{
    public class HotbarSlot : ItemSlotUI, IDropHandler
    {
        [SerializeField] private InventoryRPG inventory = null;
        [SerializeField] private TextMeshProUGUI itemQuantityText = null;

        private Item slotItemHotBar = null;

        public override Item slotItem
        {
            get { return slotItemHotBar; }
            set { slotItemHotBar = value; UpdateSlotUI(); }
        }

        public bool AddItem(Item itemToAdd)
        {
            if (slotItemHotBar != null) { return false; }

            slotItem = itemToAdd;

            return true;
        }

        public void UseSlot(int index)
        {
            if (index != slotIndex) { return; }

            //Use Item
        }

        public override void OnDrop(PointerEventData eventData)
        {
            ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();

            if (itemDragHandler == null) { return; }

            InventorySlot inventorySlot = itemDragHandler.ItemSlotUI as InventorySlot;
            if (inventorySlot != null)
            {
                slotItem = inventorySlot.itemSlot.item;
                Debug.Log("InventorySLot_SlotItem");
                return;
            }

            HotbarSlot hotbarSlot = itemDragHandler.ItemSlotUI as HotbarSlot;
            if (hotbarSlot != null)
            {
                Item oldItem = slotItem;
                slotItem = hotbarSlot.slotItem;
                hotbarSlot.slotItem = oldItem;
                Debug.Log("HotbarSLot_SlotItem");
                return;
            }
        }

        public override void UpdateSlotUI()
        {
            if (slotItem == null)
            {
                EnableSlotUI(false);
                return;
            }

            icon.sprite = slotItem.Icon;

            EnableSlotUI(true);

            SetItemQuantityUI();
        }

        private void SetItemQuantityUI()
        {
            if (slotItem is ItemRPG inventoryItem)
            {
                if (inventory.HasItem(inventoryItem))
                {
                    int quantityCount = inventory.GetTotalQuantity(inventoryItem);
                    itemQuantityText.text = quantityCount > 1 ? quantityCount.ToString() : "";
                }
                else
                    slotItem=null;
            }
            else
            {
                itemQuantityText.enabled= false;
            }
        }

        protected override void EnableSlotUI(bool enable)
        {
            base.EnableSlotUI(enable);
            itemQuantityText.enabled= enable;
        }
    }
}