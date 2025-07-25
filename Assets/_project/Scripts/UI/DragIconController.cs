using UnityEngine;
using UnityEngine.UI;

public class DragIconController : MonoBehaviour
{
    // ласс, если нужно будет перет€гиватьс€ какую-то икноку (пока что это только дл€ слотов в инвентаре)
    public static DragIconController Instance;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image icon;

    private void Awake() => Instance = this;

    private void Update()
    {
        if (icon.enabled)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                Input.mousePosition,
                canvas.worldCamera,
                out pos
            );
            icon.rectTransform.localPosition = pos;
        }
    }

    public void ShowIcon(Sprite sprite)
    {
        icon.sprite = sprite;
        icon.enabled = true;
    }

    public void HideIcon()
    {
        icon.enabled = false;
    }
}
