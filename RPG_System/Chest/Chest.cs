using RpgLogic.Items;
using UnityEngine;

public class Chest : Interactable
{
    [SerializeField] private int space = 20;

    [SerializeField] private IItemContainer chestContainer;

    [SerializeField] private GameObject chestUI;

    private bool isOpen = false;
    private InventoryRPG inventory;

    [SerializeField] private Transform chestItemParent;

    public override void Interact()
    {
        base.Interact();
        OpenChestWindow();
    }

    protected override void Update()
    {
        base.Update();

        if (!isFocus && isOpen)
        {
            CloseChestWindow();
            isOpen = false;
        }
    }

    private void OpenChestWindow()
    {
        GameControll.instance.ShowCursor();
        isOpen = true;
        chestUI.SetActive(true);
    }

    private void CloseChestWindow()
    {
        chestUI.SetActive(false);
        GameControll.instance.HideCursor();
    }
}
