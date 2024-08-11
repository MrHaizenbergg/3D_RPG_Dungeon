using RpgLogic.Items;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EquipInventory : MonoBehaviour
{
    [SerializeField] private UnityEvent onEquipInventoryItemUpdated = null;
    [SerializeField] private ItemSlot[] itemSlots = new ItemSlot[0];
    [SerializeField] private InventoryRPG inventory;

    public ItemSlot GetSlotByIndex(int index) => itemSlots[index];

    private EquipmentManager equipmentManager;

    public void EquipItem(Equipment item)
    {
        ItemSlot slot = new ItemSlot(item, 1);

        inventory.RemoveItem(slot);
        AddItem(slot);
    }

    public void UnequipItem(Equipment item)
    {
        ItemSlot slot = new ItemSlot(item, 1);

        RemoveItem(slot);
        inventory.AddItem(slot);
    }

    private void Start()
    {
        equipmentManager = EquipmentManager.Instance;
    }

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

        Equipment equip = slot.item as Equipment;

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
                        itemSlots[(int)equip.EquipSlot].quantity += slot.quantity;

                        slot.quantity = 0;

                        onEquipInventoryItemUpdated.Invoke();

                        Debug.Log("EventAdd_1");

                        return slot;
                    }
                    else if (slotRemainingSpace > 0)
                    {
                        itemSlots[(int)equip.EquipSlot].quantity += slotRemainingSpace;

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
                    itemSlots[(int)equip.EquipSlot] = slot;

                    slot.quantity = 0;

                    onEquipInventoryItemUpdated.Invoke();

                    return slot;
                }
                else
                {
                    itemSlots[(int)equip.EquipSlot] = new ItemSlot(slot.item, slot.item.MaxStack);


                    slot.quantity -= slot.item.MaxStack;
                }
            }
        }

        onEquipInventoryItemUpdated.Invoke();

        return slot;
    }

    public void RemoveItem(ItemSlot slot)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].item != null)
            {
                if (itemSlots[i].item == slot.item)
                {
                    if (itemSlots[i].quantity < slot.quantity)
                    {
                        slot.quantity -= itemSlots[i].quantity;

                        itemSlots[i] = new ItemSlot();
                    }
                    else
                    {
                        itemSlots[i].quantity -= slot.quantity;

                        if (itemSlots[i].quantity == 0)
                        {
                            itemSlots[i] = new ItemSlot();

                            onEquipInventoryItemUpdated.Invoke();

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

        onEquipInventoryItemUpdated.Invoke();
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
