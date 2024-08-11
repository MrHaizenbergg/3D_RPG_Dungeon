using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RpgLogic.Items
{
    public abstract class ItemSlotUI : MonoBehaviour, IDropHandler
    {
        [SerializeField] protected Image icon = null;
        public int slotIndex { get; private set; }

        public abstract Item slotItem { get; set; }

        private void OnEnable()
        {
            UpdateSlotUI();
        }

        protected virtual void Start()
        {
            slotIndex = transform.GetSiblingIndex();
            UpdateSlotUI();
        }

        public abstract void OnDrop(PointerEventData eventData);

        public abstract void UpdateSlotUI();

        protected virtual void EnableSlotUI(bool enable)
        {
            icon.enabled = enable;
        }
    }
}

