using UnityEngine;
using MyGame.EventBus;
using MyGame.Inventory;

public class InventoryWindow : MonoBehaviour
{
    [SerializeField] private GameObject InventoryPanel;
    [SerializeField] private InventoryUI playerInventoryUI;
    [SerializeField] private InventoryUI otherInventoryUI;

    //Тут сделать подписку на событие, которое в себе содержит два InventoryComponent, но второе может быть null
    private void OnEnable()
    {
        EventBus.Subscribe<OnInventoryInteract>(InteractInventory);
        EventBus.Subscribe<OnItemShiftClick>(HandleShiftClick);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnInventoryInteract>(InteractInventory);
        EventBus.Unsubscribe<OnItemShiftClick>(HandleShiftClick);
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

    private void HandleShiftClick(OnItemShiftClick e)
    {
        if (otherInventoryUI.gameObject.activeSelf)
        {
            InventoryComponent toInv = e.cell.Inventory == playerInventoryUI.Inventory.Inventory ? otherInventoryUI.Inventory : playerInventoryUI.Inventory;
            InventoryComponent fromInv = e.cell.Inventory == playerInventoryUI.Inventory.Inventory ? playerInventoryUI.Inventory : otherInventoryUI.Inventory;
            //Закинуть из в другой
            Services.InventoryInteractionService.TransferItem(fromInv, e.cell.index, toInv, toInv.GetFirstFreeSlotIndex(), new InteractionContext());
        }
        else
        {
            InventoryComponent inv = playerInventoryUI.Inventory;
            Services.InventoryInteractionService.TransferItem(inv, e.cell.index, inv, inv.GetFirstFreeSlotIndex(), new InteractionContext());
        }
    }
}

