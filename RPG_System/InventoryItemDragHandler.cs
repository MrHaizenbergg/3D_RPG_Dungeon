using RpgLogic.Items;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItemDragHandler : ItemDragHandler
{
    [SerializeField] private ItemDestroyer itemDestroyer = null;

    private ItemRPG itemRPG = null;

    private float lastClickTime;
    private const float DoubleClickTime = 0.3f;

    public override void OnPointerUp(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            base.OnPointerUp(pointerEventData);


            if (pointerEventData.hovered.Count == 0)
            {
                InventorySlot thisSlot = itemSlotUI as InventorySlot;
                itemDestroyer.Active(thisSlot.itemSlot, thisSlot.slotIndex, thisSlot);
            }

            if (pointerEventData.hovered.Count > 1 && !isHovering)
            {
                if (itemSlotUI.slotItem != null)
                {
                    float timeSinceLatClick = Time.time - lastClickTime;

                    if (timeSinceLatClick <= DoubleClickTime)
                    {
                        itemRPG = ItemSlotUI.slotItem as ItemRPG;
                        itemRPG.Use();

                        if (itemRPG.Modifiers.Count > 0)
                        {
                            InventoryRPG inventory = PlayerManager.instance.Player.GetComponent<InventoryRPG>();
                            if (inventory != null)
                            {
                                InventorySlot inventorySlot = ItemSlotUI as InventorySlot;
                                inventory.RemoveItemAfterUse(inventorySlot.itemSlot);
                            }
                        }
                    }

                    lastClickTime = Time.time;
                }
            }
        }
    }
}
