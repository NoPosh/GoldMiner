using UnityEngine;
using MyGame.EventBus;
using MyGame.Inventory;
using UnityEngine.UI;

public class InventoryWindow : MonoBehaviour
{
    [SerializeField] private GameObject InventoryPanel;
    [SerializeField] private InventoryUI playerInventoryUI;
    [SerializeField] private InventoryUI otherInventoryUI;
    [SerializeField] private RecycleInventoryUI recycleInventoryUI;

    [SerializeField] private Button recycleButton;

    //Тут сделать подписку на событие, которое в себе содержит два InventoryComponent, но второе может быть null
    private void OnEnable()
    {
        EventBus.Subscribe<OnInventoryInteract>(InteractInventory);
        EventBus.Subscribe<OnItemShiftClick>(HandleShiftClick);
        EventBus.Subscribe<OnInteractRecycle>(InteractInventory);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnInventoryInteract>(InteractInventory);
        EventBus.Unsubscribe<OnItemShiftClick>(HandleShiftClick);
        EventBus.Unsubscribe<OnInteractRecycle>(InteractInventory);
    }

    private void Awake()
    {
        Hide(InventoryPanel);
    }

    public void Show(InventoryComponent playerInv, InventoryComponent otherInv = null)
    {
        InventoryPanel.SetActive(true);

        playerInventoryUI.Bind(playerInv);

        if (otherInv != null)
        {
            otherInventoryUI.Bind(otherInv);
            otherInventoryUI.gameObject.SetActive(true);
        }
        else Hide(otherInventoryUI.gameObject);
    }

    public void Show(InventoryComponent playerInv, RecyclerComponent recycleInv)
    {
        InventoryPanel.SetActive(true);

        playerInventoryUI.Bind(playerInv);

        recycleInventoryUI.Bind(recycleInv);
        recycleInventoryUI.gameObject.SetActive(true);
        recycleButton.onClick.AddListener(SwitchRecycle);
    }

    public void Hide(GameObject obj)
    {
        obj.SetActive(false); 
    }

    private void CloseInventory()
    {
        Hide(InventoryPanel);
        Hide(otherInventoryUI.gameObject);
        if (recycleInventoryUI.gameObject.activeSelf)
        {
            Hide(recycleInventoryUI.gameObject);
            recycleButton.onClick.RemoveAllListeners();
        }        
        Cursor.lockState = CursorLockMode.Locked;
        EventBus.Raise<OnInventoryClose>(new OnInventoryClose());
    }

    private void InteractInventory(OnInventoryInteract e)   //Можно сделать через интерфейс. Чтобы в ивенте передавался интерфейс, который может: Выдать оба инвентаря
    {
        if (InventoryPanel.gameObject.activeSelf)
        {
            CloseInventory();
        }
        else
        {
            Show(e.playerInv, e.otherInv);
            Cursor.lockState = CursorLockMode.None;
            EventBus.Raise<OnInventoryOpen>(new OnInventoryOpen());
        }
    }

    private void InteractInventory(OnInteractRecycle e)
    {
        if (InventoryPanel.gameObject.activeSelf)
        {
            CloseInventory();
        }
        else
        {
            Show(e.playerInv, e.recycler);
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
            Services.InventoryInteractionService.TransferItem(fromInv, e.cell.index, toInv, toInv.GetFirstFreeSlotIndex(), new InteractionContext());
        }
        else if (recycleInventoryUI.gameObject.activeSelf)
        {
            InventoryComponent toInv = e.cell.Inventory == playerInventoryUI.Inventory.Inventory ? recycleInventoryUI.Inventory : playerInventoryUI.Inventory;
            InventoryComponent fromInv = e.cell.Inventory == playerInventoryUI.Inventory.Inventory ? playerInventoryUI.Inventory : recycleInventoryUI.Inventory;

            Services.InventoryInteractionService.TransferItem(fromInv, e.cell.index, toInv, toInv.GetFirstFreeSlotIndex(), new InteractionContext { IsRecycler = true});
        }
        else
        {
            InventoryComponent inv = playerInventoryUI.Inventory;
            Services.InventoryInteractionService.TransferItem(inv, e.cell.index, inv, inv.GetFirstFreeSlotIndex(), new InteractionContext());
        }
    }

    private void SwitchRecycle()
    {
        
        RecyclerComponent rec = recycleInventoryUI.Recycler;
        if (rec.Recycler.IsProcessing)
        {
            rec.StopProcess();
        }
        else
        {
            rec.StartProcess();
        }
    }
}

