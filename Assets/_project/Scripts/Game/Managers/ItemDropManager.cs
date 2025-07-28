using UnityEngine;
using MyGame.EventBus;
using MyGame.Inventory;
using System;

public class ItemDropManager : MonoBehaviour
{
    //Слушает событие OnItemDrop и выбрасывает предмет, который надо
    private void OnEnable()
    {
        EventBus.Subscribe<OnItemDropped>(DropObject);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe<OnItemDropped>(DropObject);
    }

    private void DropObject(OnItemDropped e)
    {
        var dropped = Instantiate(e.Item.itemPrefab, e.Position, Quaternion.identity);
        dropped.GetComponent<WorldItem>().amount = e.Amount;

        if (dropped.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.AddForce(e.Force, ForceMode.Impulse);
        }
    }
}
