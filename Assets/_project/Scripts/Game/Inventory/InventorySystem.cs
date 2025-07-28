using UnityEngine;
using MyGame.EventBus;
using UnityEngine.InputSystem;

public class InventorySystem : MonoBehaviour
{
    private InventoryComponent inventory;
    private InputAction inventoryAction;

    private void Awake()
    {
        inventory = GetComponent<InventoryComponent>();
        inventoryAction = InputSystem.actions.FindAction("Inventory");
        EventBus.Subscribe<ItemPickupAttemptEvent>(OnPickupAttempt);
        //EventBus.Subscribe<OnItemShiftClick>(HandleShiftClick);
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe<ItemPickupAttemptEvent>(OnPickupAttempt);
        //EventBus.Unsubscribe<OnItemShiftClick>(HandleShiftClick);
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
            EventBus.Raise<OnInventoryInteract>(new OnInventoryInteract(inventory));
        }
    }

    /*
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
    */

}
