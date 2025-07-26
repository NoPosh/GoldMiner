using MyGame.EventBus;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemInfo : MonoBehaviour
{
    
    [SerializeField] GameObject itemInfoPanel;
    [SerializeField] Image itemImage;
    [SerializeField] TMP_Text itemTitle;
    [SerializeField] TMP_Text itemDiscription;

    private bool IsPanelOpened => itemInfoPanel.activeSelf;

    private void OnEnable()
    {
        EventBus.Subscribe<OnItemPointerEnter>(ShowInfo);
        EventBus.Subscribe<OnItemPointerExit>(CloseInfoPanel);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnItemPointerEnter>(ShowInfo);
        EventBus.Unsubscribe<OnItemPointerExit>(CloseInfoPanel);
    }

    private void Start()
    {
        itemInfoPanel.SetActive(false);
    }

    private void OpenInfoPanel()
    {
        itemInfoPanel.SetActive(true);
    }

    private void CloseInfoPanel()
    {
        itemInfoPanel.SetActive(false);
    }

    private void ShowInfo(OnItemPointerEnter e)
    {
        if (e.cell.item != null)
        {
            itemImage.sprite = e.cell.item.icon;
            itemTitle.text = e.cell.item.itemName;
            itemDiscription.text = e.cell.item.itemDiscription;
            //TODO: + текст с amount, price и тд
            if (!IsPanelOpened) OpenInfoPanel();
        }
       
    }
}
