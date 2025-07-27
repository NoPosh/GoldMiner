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

public struct OnOpenChest   //Открытие сундука
{
    public InventoryComponent Inventory;
    public OnOpenChest (InventoryComponent inventory)
    { 
        this.Inventory = inventory; 
    }
}

public struct OnOpenRecycle
{
    public Recycler recycler;
    public OnOpenRecycle(Recycler recycler)
    {
        this.recycler = recycler;
    }
}

public struct OnInventoryChanged    //Если что-то меняется в инвентаре
{

}

public struct OnInventoryInteract   //Нажата кнопка инвентаря
{

}

public struct OnInventoryOpen
{

}

public struct OnInventoryClose
{

}

public struct OnItemPointerEnter
{
    public InventoryCell cell;
    public OnItemPointerEnter(InventoryCell cell)
    {
        this.cell = cell;
    }
}

public struct OnItemShiftClick
{
    public InventoryCell cell;
    public OnItemShiftClick(InventoryCell cell)
    {
        this.cell = cell;
    }
        

}

public struct OnItemPointerExit
{

}

public struct OnOreCollectedGloabal
{
    public Ore ore;
    public OnOreCollectedGloabal(Ore ore)
    {
        this.ore = ore;
    }
}
