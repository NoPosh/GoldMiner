using UnityEngine;
using MyGame.EventBus;

public class InventoryWindow : MonoBehaviour
{
    [SerializeField] private GameObject InventoryPanel;
    [SerializeField] private InventoryUI playerInventoryUI;
    [SerializeField] private InventoryUI otherInventoryUI;

    //Тут сделать подписку на событие, которое в себе содержит два InventoryComponent, но второе может быть null
    private void OnEnable()
    {
        EventBus.Subscribe<OnInventoryInteract>(InteractInventory);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnInventoryInteract>(InteractInventory);
    }

    private void Awake()
    {
        Hide(InventoryPanel);
    }

    public void Show(InventoryComponent playerInv, InventoryComponent otherInv = null)
    {
        InventoryPanel.SetActive(true);
        gameObject.SetActive(true);

        playerInventoryUI.Bind(playerInv);

        if (otherInv != null)
        {
            otherInventoryUI.Bind(otherInv);
            otherInventoryUI.gameObject.SetActive(true);
        }
        else Hide(otherInventoryUI.gameObject);
    }

    public void Hide(GameObject obj)
    {
        obj.SetActive(false); 
    }

    private void InteractInventory(OnInventoryInteract e)
    {
        if (InventoryPanel.gameObject.activeSelf)
        {
            Hide(InventoryPanel);
            Cursor.lockState = CursorLockMode.Locked;
            EventBus.Raise<OnInventoryClose>(new OnInventoryClose());
        }
        else
        {
            Show(e.playerInv, e.otherInv);
            Cursor.lockState = CursorLockMode.None;
            EventBus.Raise<OnInventoryOpen>(new OnInventoryOpen());
        }
    }
}

