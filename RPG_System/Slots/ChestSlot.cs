using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RpgLogic.Items
{
    public class ChestSlot : ItemSlotUI, IDropHandler
    {
        private protected HoverInfoPopUp infoPopUp;

        [SerializeField] private InventoryRPG inventory = null;
        [SerializeField] private InventoryRPG playerInventory= null;
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
            playerInventory=PlayerManager.instance.Player.GetComponent<InventoryRPG>();
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
            ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();

            if (itemDragHandler == null) { return; }

            if ((itemDragHandler.ItemSlotUI as ChestSlot) != null)
            {
                inventory.Swap(itemDragHandler.ItemSlotUI.slotIndex, slotIndex);
                Debug.Log("Swap");
            }

            InventorySlot inventorySlot = itemDragHandler.ItemSlotUI as InventorySlot;
            if (inventorySlot != null)
            {
                inventory.AddItem(inventorySlot.itemSlot);
                playerInventory.RemoveItem(inventorySlot.itemSlot);
                Debug.Log("InventorySLot_SlotItem");
                return;
            }
        }
    }
}

