using UnityEngine;
using MyGame.EventBus;
using UnityEngine.InputSystem;

public class InventorySystem : MonoBehaviour
{
    private InventoryComponent inventory;
    private InventoryComponent sideInventory;

    private InputAction inventoryAction;

    private void Awake()
    {
        inventory = GetComponent<InventoryComponent>();
        inventoryAction = InputSystem.actions.FindAction("Inventory");
        EventBus.Subscribe<ItemPickupAttemptEvent>(OnPickupAttempt);
        EventBus.Subscribe<OnItemShiftClick>(HandleShiftClick);
        EventBus.Subscribe<OnOpenChest>(SetSideInventory);
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe<ItemPickupAttemptEvent>(OnPickupAttempt);
        EventBus.Unsubscribe<OnItemShiftClick>(HandleShiftClick);
        EventBus.Unsubscribe<OnOpenChest>(SetSideInventory);
    }

    private void OnPickupAttempt(ItemPickupAttemptEvent e)
    {
        if (e.picker == gameObject)
        {
            bool added = inventory.AddItem(e.item, e.amount);
            e.onResult?.Invoke(added);  //Сообщаем результат обратно (вызываем передаваемый делегат)
        }
    }

    private void Update()
    {
        if (inventoryAction.triggered)
        {
            EventBus.Raise<OnInventoryInteract>(new OnInventoryInteract());
        }
    }

    private void HandleShiftClick(OnItemShiftClick e)
    {
        if (UIManager.Instance._sideInventoryPanelActive)
        {
            if (e.cell.Inventory == inventory)
            {
                InventoryComponent.MoveItemBetween(inventory, e.cell.index, sideInventory, sideInventory.GetFirstFreeSlotIndex());
            }
            else
            {
                InventoryComponent.MoveItemBetween(sideInventory, e.cell.index, inventory, inventory.GetFirstFreeSlotIndex());
            }                    
        }
        else 
        {
            inventory.MoveItem(e.cell.index, inventory.GetFirstFreeSlotIndex());
        }
    }

    private void SetSideInventory(OnOpenChest e)
    {
        sideInventory = e.Inventory;
    }
}
