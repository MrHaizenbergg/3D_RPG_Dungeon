using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RpgLogic.Items
{
    public class EquipSlot : ItemSlotUI, IDropHandler
    {
        private protected HoverInfoPopUp infoPopUp;

        [SerializeField] private EquipInventory inventory = null;
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
        }

        public ItemSlot itemSlot => inventory.GetSlotByIndex(slotIndex);

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
            //ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();

            //if (itemDragHandler == null) { return; }

            //InventorySlot inventorySlot = itemDragHandler.ItemSlotUI as InventorySlot;
            //if (inventorySlot != null)
            //{
            //    slotItem = inventorySlot.itemSlot.item;
            //    return;
            //}

            //EquipSlot equipSlot = itemDragHandler.ItemSlotUI as EquipSlot;
            //if (equipSlot != null)
            //{
            //    Item oldItem = slotItem;
            //    slotItem = equipSlot.slotItem;
            //    equipSlot.slotItem = oldItem;
            //    return;
            //}
        }
    }
}