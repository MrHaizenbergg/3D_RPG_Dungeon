using System.Collections.Generic;

namespace RpgLogic.Items
{
    public interface IItemContainer
    {
        int Money {  get; set; }

        ItemSlot GetSlotByIndex(int index);

        List<ItemRPG> GetAllUniqueItems();

        ItemSlot AddItem(ItemSlot slot);

        void RemoveItem(ItemSlot slot);

        void RemoveAt(int slotIndex);

        void Swap(int indexOne, int indexTwo);

        bool HasItem(ItemRPG item);

        int GetTotalQuantity(ItemRPG item);

    }
}

