using UnityEngine;
using UnityEngine.EventSystems;

namespace RpgLogic.Items.Hotbars
{
    public class HotbarItemDragHandler : ItemDragHandler
    {
        private ItemRPG itemRPG = null;

        public override void OnPointerUp(PointerEventData pointerEventData)
        {
            if (pointerEventData.button == PointerEventData.InputButton.Left)
            {
                base.OnPointerUp(pointerEventData);

                if (pointerEventData.hovered.Count == 0)
                {
                    (itemSlotUI as HotbarSlot).slotItem = null;
                }

                if (pointerEventData.hovered.Count > 1)
                {
                    itemRPG = ItemSlotUI.slotItem as ItemRPG;
                    itemRPG.Use();

                    if (itemRPG.Modifiers.Count > 0)
                    {
                        HotbarSlot hotbarSlot = ItemSlotUI as HotbarSlot;

                        InventoryRPG inventory = PlayerManager.instance.Player.GetComponent<InventoryRPG>();
                        inventory.RemoveItemAfterUse(inventory.GetItemSlot(itemRPG));
                    }
                }
            }
        }
    }
}
