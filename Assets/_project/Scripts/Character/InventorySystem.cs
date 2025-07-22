using UnityEngine;
using MyGame.EventBus;

public class InventorySystem : MonoBehaviour
{
    private InventoryComponent inventory;

    private void Awake()
    {
        inventory = GetComponent<InventoryComponent>();
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
}
