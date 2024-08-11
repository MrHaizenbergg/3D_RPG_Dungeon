using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class InventoryRPG_UI : MonoBehaviour
{
    public GameObject inventoryUI;
    public GameObject equipUI;
    public GameControll gameControll;

    InventoryRPG inventory;

    private void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Inventory"))
        {
            if (inventoryUI.activeSelf)
            {
                gameControll.HideCursor();
              
                inventoryUI.SetActive(false);
                equipUI.SetActive(false);
            }
            else
            {
                gameControll.ShowCursor();
               
                inventoryUI.SetActive(true);
                equipUI.SetActive(true);
            }
        }
    }
}
