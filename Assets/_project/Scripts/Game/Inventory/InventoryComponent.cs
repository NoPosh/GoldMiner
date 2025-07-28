using UnityEngine;
using System.Collections.Generic;
using MyGame.EventBus;
using MyGame.Inventory;

public class InventoryComponent : MonoBehaviour
{
    [SerializeField] protected int size = 20;
    [SerializeField] protected Transform dropPoint;
    [SerializeField] protected float dropForce = 5f; //Мб сделать что-то типо мини игры с силой кидания

    public int Size => size;
    private Inventory inventory;
    public Inventory Inventory { get { return inventory; } }

    private bool IsInitialized = false;


    protected virtual void Awake()
    {
        inventory = new Inventory(size);
        inventory.OnInventoryChanged += () => EventBus.Raise(new OnInventoryChanged());
    }

    public void Initialize(Inventory inventory)
    {
        if (IsInitialized) return;
        this.inventory = inventory;
        IsInitialized = true;
    }

    public bool AddItem(BaseItem newItem, int amount = 1)
    {

        return inventory.AddItem(newItem, amount);
    }

    public virtual void RemoveItem(int index, int amount = 1)
    {
        inventory.RemoveItem(index, amount);
    }
    
    //Перемещение внутри одного инвентаря
    public virtual bool MoveItem(int fromIndex, int toIndex)
    {
        return inventory.MoveItem(fromIndex, toIndex);
    }

    //Перемещение между инвентарями
    public bool MoveItemBetween(InventoryComponent toInv, int fromIndex, int toIndex, InteractionContext context)
    {
        return Services.InventoryInteractionService.TransferItem(this, fromIndex, toInv, toIndex, context);
    }

    public virtual void DropItem(int index, int amount = 1) //Подумать о том, чтобы использовать пулл объектов
    {
        var cell = inventory.GetCell(index);
        if (cell.IsEmpty) return;
        if (dropPoint == null) 
        {
            Vector3 pointInFront = transform.position + transform.forward * 1f;
            GameObject pointObject = new GameObject("DropPoint");
            pointObject.transform.position = pointInFront;
            dropPoint = pointObject.transform;
        }
        EventBus.Raise<OnItemDropped>(new OnItemDropped(cell.item, cell.amount, dropPoint.position, dropPoint.forward * dropForce));
        RemoveItem(index);
    }

    public virtual void UseItem(int index)
    {

    }

    public virtual BaseItem GetItem(int index)
    {
        return inventory.GetCell(index).item;
    }   //Надо ли

    public virtual int GetFirstFreeSlotIndex()
    {
        foreach (var cell in inventory.Cells)
        {
            if (cell.IsEmpty)
            {
                return cell.index;
            }
        }
        return -1;
    }
}


