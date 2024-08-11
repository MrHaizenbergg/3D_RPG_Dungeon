using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RpgLogic.Items
{
    public class InventorySlot : ItemSlotUI, IDropHandler
    {
        private protected HoverInfoPopUp infoPopUp;

        [SerializeField] private InventoryRPG inventory = null;
        [SerializeField] private InventoryRPG chestInventory= null;
        [SerializeField] private TextMeshProUGUI itemQuantityText = null;

        public override Item slotItem
        {
            get { return itemSlot.item; }
            set { }
        }

        protected override void Start()
        {
            base.Start();
            infoPopUp = FindObjectOfType<HoverInfoPopUp>();
            chestInventory = PlayerManager.instance.Chest.GetComponent<InventoryRPG>();
        }

        public ItemSlot itemSlot => inventory.GetSlotByIndex(slotIndex);

        public void UseItemInSlot()
        {
            if (slotItem != null)
            {
                ItemRPG itemRPG = slotItem as ItemRPG;
                itemRPG.Use();
            }
        }

        public override void UpdateSlotUI()
        {
            if (itemSlot.item == null)
            {
                EnableSlotUI(false);
                return;
            }

            EnableSlotUI(true);

            icon.sprite = itemSlot.item.Icon;
            itemQuantityText.text = itemSlot.quantity > 1 ? itemSlot.quantity.ToString() : "";
        }

        protected override void EnableSlotUI(bool enable)
        {
            base.EnableSlotUI(enable);
            itemQuantityText.enabled = enable;
        }

        public override void OnDrop(PointerEventData eventData)
        {
            ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();

            if (itemDragHandler == null) { return; }

            if ((itemDragHandler.ItemSlotUI as InventorySlot) != null)
            {
                inventory.Swap(itemDragHandler.ItemSlotUI.slotIndex, slotIndex);
                Debug.Log("Swap");
            }

            ChestSlot chestSlot = itemDragHandler.ItemSlotUI as ChestSlot;
            if (chestSlot != null)
            {
                inventory.AddItem(chestSlot.itemSlot);
                chestInventory.RemoveItem(chestSlot.itemSlot);
                Debug.Log("ChestSLot_SlotItem");
                return;
            }
        }
    }
}