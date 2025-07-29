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
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe<ItemPickupAttemptEvent>(OnPickupAttempt);
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

}
