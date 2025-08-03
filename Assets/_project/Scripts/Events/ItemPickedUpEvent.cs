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

public struct OnInteractRecycle
{
    public InventoryComponent playerInv;
    public RecyclerComponent recycler;
    public OnInteractRecycle(InventoryComponent playerInv, RecyclerComponent recycler)
    {
        this.playerInv = playerInv;
        this.recycler = recycler;
    }
}

public struct OnItemDropped
{
    public BaseItem Item;
    public int Amount;
    public Vector3 Position;
    public Vector3 Force;
    public OnItemDropped(BaseItem item, int amount, Vector3 pos, Vector3 force)
    {
        this.Item = item;
        this.Amount = amount;
        this.Position = pos;
        this.Force = force;
    }
}

public struct OnInventoryChanged    //Если что-то меняется в инвентаре
{

}

public struct OnInventoryInteract   //Нажата кнопка инвентаря
{
    public InventoryComponent playerInv;
    public InventoryComponent otherInv;
    public InteractionContext context;

    public OnInventoryInteract(InventoryComponent player, InventoryComponent other = null, InteractionContext context = null)
    {
        playerInv = player;
        otherInv = other;
        if (context != null)
            this.context = context;
        else this.context = new InteractionContext();
    }
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

public struct OnDialogieStart
{
    public Dialogue dialogue;
    public DialogueContext context;
    public OnDialogieStart (Dialogue dialogue, DialogueContext context)
    {
        this.dialogue = dialogue;
        this.context = context;
    }
}
