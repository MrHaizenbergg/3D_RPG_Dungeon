using RpgLogic.Items;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChestItemDragHandler : ItemDragHandler
{
    [SerializeField] private ItemDestroyer itemDestroyer = null;
    [SerializeField] private Canvas upUiElement;

    private ItemRPG itemRPG = null;

    public override void OnPointerUp(PointerEventData pointerEventData)
    {
        upUiElement.sortingOrder = 5;

        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            base.OnPointerUp(pointerEventData);

            //itemRPG = ItemSlotUI.slotItem as ItemRPG;
            //itemRPG.Use();

            if (pointerEventData.hovered.Count == 0)
            {
                ChestSlot thisSlot = itemSlotUI as ChestSlot;
                itemDestroyer.Active(thisSlot.itemSlot, thisSlot.slotIndex,thisSlot);
            }
        }
    }

    public override void OnDrag(PointerEventData pointerEventData)
    {
        base.OnDrag(pointerEventData);

        upUiElement.sortingOrder = 7;
    }
}
