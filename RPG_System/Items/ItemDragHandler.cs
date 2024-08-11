using RpgLogic.Events.CustomEvents;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RpgLogic.Items
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ItemDragHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler
    {
        [SerializeField] protected ItemSlotUI itemSlotUI = null;
        [SerializeField] protected ItemEvent onMouseStartHoverItem=null;
        [SerializeField] protected VoidEvent onMouseEndHoverItem=null;

        private CanvasGroup canvasGroup = null;
        private Transform originalParent = null;

        public bool isHovering = false;

        public ItemSlotUI ItemSlotUI => itemSlotUI;

        public virtual void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnDisable()
        {
            if (isHovering)
            {
                onMouseEndHoverItem.Raise();
                isHovering = false;
            }
        }

        public virtual void OnPointerDown(PointerEventData pointerEventData)
        {
            if (pointerEventData.button == PointerEventData.InputButton.Left)
            {
                onMouseEndHoverItem.Raise();

                originalParent = transform.parent;

                transform.SetParent(transform.parent.parent);

                canvasGroup.blocksRaycasts = false;
                Debug.Log("PointerDown");
            }
        }
        public virtual void OnDrag(PointerEventData pointerEventData)
        {
            if (pointerEventData.button == PointerEventData.InputButton.Left)
            {
                transform.position = Input.mousePosition;
            }
        }
        public virtual void OnPointerUp(PointerEventData pointerEventData)
        {
            if (pointerEventData.button == PointerEventData.InputButton.Left)
            {
                transform.SetParent(originalParent);
                transform.localPosition = Vector3.zero;
                canvasGroup.blocksRaycasts = true;
                Debug.Log("PointerUp");
            }
        }
        public virtual void OnPointerEnter(PointerEventData pointerEventData)
        {
            onMouseStartHoverItem.Raise(itemSlotUI.slotItem);
            isHovering = true;
            Debug.Log("PointEnterID");
        }
        public virtual void OnPointerExit(PointerEventData pointerEventData)
        {
            onMouseEndHoverItem.Raise();
            isHovering = false;
            Debug.Log("PointExitID");
        }
    }
}

