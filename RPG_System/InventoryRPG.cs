using RpgLogic.Items;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryRPG : MonoBehaviour, IItemContainer
{
    [SerializeField] private int money = 100;
    [SerializeField] private UnityEvent onInventoryItemUpdated = null;
    [SerializeField] private ItemSlot[] itemSlots = new ItemSlot[0];

    public int Money { get { return money; } set { money = value; } }

    public ItemSlot GetSlotByIndex(int index) => itemSlots[index];

    public List<ItemRPG> GetAllUniqueItems()
    {
        List<ItemRPG> items = new List<ItemRPG>();

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].item == null) { continue; }

            if (items.Contains(itemSlots[i].item)) { continue; }

            items.Add(itemSlots[i].item);

        }

        return items;
    }

    public static void ItemCheck(ItemRPG item)
    {
        //if (OnItemCheck != null)
        //OnItemCheck(item);

    }

    public ItemSlot AddItem(ItemSlot slot)
    {
        if (slot.item.IsDefaultItem)
            return slot;

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].item != null)
            {
                if (itemSlots[i].item == slot.item)
                {
                    int slotRemainingSpace = itemSlots[i].item.MaxStack - itemSlots[i].quantity;

                    Debug.Log("SlotRemSpace: " + slotRemainingSpace);

                    if (slot.quantity <= slotRemainingSpace)
                    {
                        itemSlots[i].quantity += slot.quantity;

                        slot.quantity = 0;

                        //if (onItemChangedCallback != null)
                        //    onItemChangedCallback.Invoke();

                        onInventoryItemUpdated.Invoke();

                        Debug.Log("EventAdd_1");

                        return slot;
                    }
                    else if (slotRemainingSpace > 0)
                    {
                        itemSlots[i].quantity += slotRemainingSpace;

                        slot.quantity -= slotRemainingSpace;
                    }
                }
            }
        }

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].item == null)
            {
                if (slot.quantity <= slot.item.MaxStack)
                {
                    itemSlots[i] = slot;

                    slot.quantity = 0;

                    //if (onItemChangedCallback != null)
                    //    onItemChangedCallback.Invoke();
                    onInventoryItemUpdated.Invoke();

                    Debug.Log("EventAdd_2");

                    return slot;
                }
                else
                {
                    itemSlots[i] = new ItemSlot(slot.item, slot.item.MaxStack);


                    slot.quantity -= slot.item.MaxStack;
                }
            }
        }

        //if (onItemChangedCallback != null)
        //    onItemChangedCallback.Invoke();
        onInventoryItemUpdated.Invoke();

        Debug.Log("EventAdd_3");

        return slot;
    }

    public void RemoveItemAfterUse(ItemSlot slot)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].item != null)
            {
                if (itemSlots[i].item == slot.item)
                {
                    if (itemSlots[i].quantity==0)
                    {
                        itemSlots[i] = new ItemSlot();
                        onInventoryItemUpdated.Invoke();

                        return;
                    }
                    if (itemSlots[i].quantity>0)
                    {
                        itemSlots[i].quantity -= 1;
                                   
                        if (itemSlots[i].quantity==0)
                        {
                            itemSlots[i] = new ItemSlot();
                            onInventoryItemUpdated.Invoke();
                            return;
                        }
                       
                        onInventoryItemUpdated.Invoke();

                        return;
                    }
                }
            }
        }
    }

    public void RemoveItem(ItemSlot slot)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].item != null)
            {
                if (itemSlots[i].item == slot.item)
                {
                    Debug.Log("InventSlot==SLot");

                    if (itemSlots[i].quantity < slot.quantity)
                    {
                        slot.quantity -= itemSlots[i].quantity;

                        itemSlots[i] = new ItemSlot();
                        Debug.Log("RemoveSlotInventQuant<: " + itemSlots[i].item.name);
                    }
                    else
                    {
                        itemSlots[i].quantity -= slot.quantity;

                        if (itemSlots[i].quantity == 0)
                        {
                            itemSlots[i] = new ItemSlot();

                            //if (onItemChangedCallback != null)
                            //    onItemChangedCallback.Invoke();
                            onInventoryItemUpdated.Invoke();

                            return;
                        }
                    }
                }
            }
        }
    }

    public void RemoveAt(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex > itemSlots.Length - 1) { return; }

        Debug.Log("Slot: " + slotIndex);

        itemSlots[slotIndex] = new ItemSlot();

        //if (onItemChangedCallback != null)
        //    onItemChangedCallback.Invoke();
        onInventoryItemUpdated.Invoke();
    }

    public void Swap(int indexOne, int indexTwo)
    {
        ItemSlot firstSlot = itemSlots[indexOne];
        ItemSlot secondSlot = itemSlots[indexTwo];

        if (firstSlot.Equals(secondSlot)) return;

        if (secondSlot.item != null)
        {
            if (firstSlot.item == secondSlot.item)
            {
                int secondSlotRemainingSpace = secondSlot.item.MaxStack - secondSlot.quantity;

                if (firstSlot.quantity <= secondSlotRemainingSpace)
                {
                    itemSlots[indexTwo].quantity += firstSlot.quantity;

                    itemSlots[indexOne] = new ItemSlot();

                    //if (onItemChangedCallback != null)
                    //    onItemChangedCallback.Invoke();
                    onInventoryItemUpdated.Invoke();

                    Debug.Log("EventSwap_1");

                    return;
                }
            }
        }

        itemSlots[indexOne] = secondSlot;
        itemSlots[indexTwo] = firstSlot;

        //if (onItemChangedCallback != null)
        //    onItemChangedCallback.Invoke();
        onInventoryItemUpdated.Invoke();
        Debug.Log("EventSwap_2");
    }

    public bool HasItem(ItemRPG item)
    {
        foreach (ItemSlot slot in itemSlots)
        {
            if (slot.item == null) continue;
            if (slot.item != item) continue;

            return true;
        }

        return false;
    }

    public ItemSlot GetItemSlot(ItemRPG item)
    {
        foreach (ItemSlot slot in itemSlots)
        {
            if (slot.item == null) continue;
            if (slot.item != item) continue;

            return slot;
        }

        return default;
    }

    public int GetTotalQuantity(ItemRPG item)
    {
        int totalCount = 0;

        foreach (ItemSlot slot in itemSlots)
        {
            if (slot.item == null) continue;
            if (slot.item != item) continue;

            totalCount += slot.quantity;
        }

        return totalCount;
    }
}
