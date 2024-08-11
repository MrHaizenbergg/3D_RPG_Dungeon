using System;
using RpgLogic.Items;

[Serializable]
public struct ItemSlot
{
    public ItemRPG item;
    public int quantity;

    public ItemSlot (ItemRPG item,int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }
}
