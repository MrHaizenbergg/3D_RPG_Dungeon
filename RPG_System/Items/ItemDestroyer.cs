using RpgLogic.Items;
using TMPro;
using UnityEngine;

public class ItemDestroyer : MonoBehaviour
{
    [SerializeField] private InventoryRPG inventory = null;
    [SerializeField] private InventoryRPG inventoryChest = null;
    [SerializeField] private TextMeshProUGUI areYouSureText = null;

    private int slotIndex = 0;
    private bool isChestSlot = false;

    private void OnDisable()
    {
        slotIndex = -1;
    }

    public void Active(ItemSlot itemSlot, int slotIndex, ItemSlotUI slot)
    {
        this.slotIndex = slotIndex;

        if (slot as ChestSlot)
            isChestSlot = true;
        else if (slot as InventorySlot)
            isChestSlot = false;

        //if (Language.Instance.currentLanguage == "en")
        //
        //areYouSureText.text = $"Are you sure you wish to destroy {itemSlot.quantity}x {itemSlot.item.ColouredName}?";
        //}
        //else if (Language.Instance.currentLanguage == "ru")
        //{
        //areYouSureText.text = $"Вы уверены что хотите уничтожить {itemSlot.quantity}x {itemSlot.item.ColouredName}?";
        //}
        //else
        //{
        areYouSureText.text = $"Are you sure you wish to destroy {itemSlot.quantity}x {itemSlot.item.ColouredName}?";
        //}

        gameObject.SetActive(true);
    }

    public void Destory()
    {
        if (isChestSlot)
            inventoryChest.RemoveAt(slotIndex);
        else
            inventory.RemoveAt(slotIndex);

        gameObject.SetActive(false);
    }
}
