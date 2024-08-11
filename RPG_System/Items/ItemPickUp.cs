using UnityEngine;

namespace RpgLogic.Items
{
    public class ItemPickUp : Interactable
    {
        [SerializeField] private ItemSlot itemSlot;

        [SerializeField] private InventoryRPG inventoryRPG;

        public override void Interact()
        {
            base.Interact();
            PickUp();
        }

        private void PickUp()
        {
            Debug.Log("PickUp " + itemSlot.item.name);
            var itemContainer = player.GetComponent<IItemContainer>();

            if (itemContainer == null || player==null) { return; }

            if (itemContainer.AddItem(itemSlot).quantity == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}