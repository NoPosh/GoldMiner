using UnityEngine;
using MyGame.EventBus;
using UnityEngine.InputSystem;

public class InventorySystem : MonoBehaviour
{
    private InventoryComponent inventory;
    private InputAction inventoryAction;

    private bool canMove = true;

    private void Awake()
    {
        inventory = GetComponent<InventoryComponent>();
        inventoryAction = InputSystem.actions.FindAction("Inventory");
        EventBus.Subscribe<ItemPickupAttemptEvent>(OnPickupAttempt);
        EventBus.Subscribe<OnChangeCharacterInput>(ToggleInput);
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe<ItemPickupAttemptEvent>(OnPickupAttempt);
        EventBus.Unsubscribe<OnChangeCharacterInput>(ToggleInput);
    }

    private void OnPickupAttempt(ItemPickupAttemptEvent e)
    {
        if (e.picker == gameObject)
        {
            bool added = inventory.AddItem(e.item, e.amount) != e.amount;
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

    private void ToggleInput(OnChangeCharacterInput e)
    {
        canMove = e.CanMove;
    }
}
