using System;
using UnityEngine;

[System.Obsolete]
public struct ItemPickedUpEvent
{
    public GameObject picker;
    public BaseItem item;
    public int amount;

    public ItemPickedUpEvent(GameObject picker, BaseItem item, int amount)
    {
        this.picker = picker;
        this.item = item;
        this.amount = amount;
    }
}

public struct ItemPickupAttemptEvent
{
    public GameObject picker;
    public BaseItem item;
    public int amount;
    public Action<bool> onResult;  // Callback для результата
}

public struct OnOpenChest
{
    public InventoryComponent Inventory;
    public OnOpenChest (InventoryComponent inventory)
    { 
        this.Inventory = inventory; 
    }
}

public struct OnInventoryChanged
{

}

public struct OnInventoryInteract
{

}

public struct OnInventoryOpen
{

}

public struct OnInventoryClose
{

}
