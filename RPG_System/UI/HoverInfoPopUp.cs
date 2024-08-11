using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoverInfoPopUp : MonoBehaviour
{
    [SerializeField] private GameObject popupCanvasObject = null;
    [SerializeField] private RectTransform popupObject = null;
    [SerializeField] private TextMeshProUGUI infoText = null;

    [SerializeField] private Vector3 offset = new Vector3(0f, 50f, 0f);
    [SerializeField] private float padding = 25f;

    private Canvas popupCanvas = null;

    private void Start()
    {
        popupCanvas = popupCanvasObject.GetComponent<Canvas>();
    }

    private void Update()
    {
        FollowCursor();
    }

    private void FollowCursor()
    {
        if (!popupCanvasObject.activeSelf) { return; }

        Vector3 newPos = Input.mousePosition + offset;
        newPos.z = 0f;

        float rightEdgeToScreenEdgeDistance = Screen.width - (newPos.x + popupObject.rect.width * popupCanvas.scaleFactor / 2) - padding;
        if (rightEdgeToScreenEdgeDistance < 0)
        {
            newPos.x += rightEdgeToScreenEdgeDistance;
        }
        float leftEdgeToScreenEdgeDistance = 0 - (newPos.x - popupObject.rect.width * popupCanvas.scaleFactor / 2) + padding;
        if (leftEdgeToScreenEdgeDistance > 0)
        {
            newPos.x += leftEdgeToScreenEdgeDistance;
        }
        float topEndeToScreenEdgeDistance = Screen.height - (newPos.y + popupObject.rect.height * popupCanvas.scaleFactor) - padding;
        if (topEndeToScreenEdgeDistance < 0)
        {
            newPos.y += topEndeToScreenEdgeDistance;
        }
        popupObject.transform.position = newPos;
    }

    public void DisplayInfo(RpgLogic.Items.Item item)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("<size=42>").Append(item.ColouredName).Append("</size>").AppendLine();
        builder.Append(item.GetInfoDisplayText());

        infoText.text = builder.ToString();

        popupCanvasObject.SetActive(true);

        LayoutRebuilder.ForceRebuildLayoutImmediate(popupObject);
    }
    public void HideInfo()
    {
        popupCanvasObject?.SetActive(false);
    }
}
