using RpgLogic.Items;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipItemDragHandler : ItemDragHandler
{
    private Equipment itemRPG = null;

    private float lastClickTime;
    private const float DoubleClickTime = 0.3f;

    public override void OnPointerUp(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            base.OnPointerUp(pointerEventData);

            if (pointerEventData.hovered.Count == 0)
            {
                //Delete or modify item
            }

            if (pointerEventData.hovered.Count > 1 && !isHovering)
            {
                if (itemSlotUI.slotItem != null)
                {
                    float timeSinceLatClick = Time.time - lastClickTime;

                    if (timeSinceLatClick <= DoubleClickTime)
                    {
                        itemRPG = ItemSlotUI.slotItem as Equipment;
                        itemRPG.Unequip();
                       
                    }

                    lastClickTime = Time.time;
                }
            }
        }
    }
}
