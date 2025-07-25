using UnityEngine;
using System.Collections.Generic;
using MyGame.EventBus;

public class InventoryComponent : MonoBehaviour
{
    [SerializeField] private int size = 20;
    public int Size {  get { return size; } }
    public List<InventoryCell> cells = new List<InventoryCell>();

    private void Awake()
    {
        for (int i = 0; i < size; i++)
        {
            cells.Add(new InventoryCell());
        }
    }

    public bool AddItem(BaseItem newItem, int amount = 1)
    {
        if (newItem.isStackable)
        {
            foreach (InventoryCell cell in cells)
            {
                if (cell.item == newItem && cell.amount < newItem.maxStack)
                {
                    int space = newItem.maxStack - cell.amount;
                    int toAdd = Mathf.Min(space, amount);
                    cell.amount += toAdd;
                    amount -= toAdd;

                    
                    Debug.Log($"Добавили {newItem.itemName} x {toAdd} в {cells.IndexOf(cell)} слот");
                    EventBus.Raise<OnInventoryChanged>(new OnInventoryChanged());
                    if (amount <= 0) 
                        return true;
                }
            }
        }

        //Ищем пустой слот
        foreach (InventoryCell cell in cells)
        {
            if (cell.IsEmpty)
            {
                int toAdd = Mathf.Min(newItem.maxStack, amount);
                cell.item = newItem;
                cell.amount = toAdd;
                amount -= toAdd;
                Debug.Log($"Добавили {newItem.itemName} x {toAdd} в {cells.IndexOf(cell)} слот");
                EventBus.Raise<OnInventoryChanged>(new OnInventoryChanged());
                return true;
            }
        }

        Debug.Log("Нет места в инвентаре");
        return false;
    }

    public void RemoveItem(BaseItem item, int amount)
    {
        
    }
    
    //Перемещение внутри одного инвентаря
    public bool MoveItem(int fromIndex, int toIndex)
    {
        if (fromIndex == toIndex) return false;

        var fromCell = cells[fromIndex];
        var toCell = cells[toIndex];

        if (!fromCell.IsEmpty && !toCell.IsEmpty)
        {
            (toCell.item, fromCell.item) = (fromCell.item, toCell.item);
            (toCell.amount, fromCell.amount) = (fromCell.amount, toCell.amount);
        }
        else
        {
            toCell.item = fromCell.item;
            toCell.amount = fromCell.amount;

            fromCell.Clear();
        }

        EventBus.Raise<OnInventoryChanged>(new OnInventoryChanged());
        return true;
    }

    //Перемещение между инвентарями
    public static bool MoveItemBetween(InventoryComponent fromInv, int fromIndex, InventoryComponent toInv, int toIndex)
    {
        var fromCell = fromInv.cells[fromIndex];
        var toCell = toInv.cells[toIndex];

        // Обмен местами (если оба слота заняты)
        if (!fromCell.IsEmpty && !toCell.IsEmpty)
        {
            (toCell.item, fromCell.item) = (fromCell.item, toCell.item);
            (toCell.amount, fromCell.amount) = (fromCell.amount, toCell.amount);
        }
        else // Просто перенос
        {
            toCell.item = fromCell.item;
            toCell.amount = fromCell.amount;

            fromCell.Clear();
        }

        EventBus.Raise<OnInventoryChanged>(new OnInventoryChanged());
        return true;
    }

    public void DropItem(int index, int amount)
    {

    }

    public void UseItem(int index)
    {

    }
}

[System.Serializable]
public class InventoryCell          //Может быть AddItem сюда вшить, как и остальное
{
    public BaseItem item;
    public int amount;

    public bool IsEmpty => item == null || amount <= 0;

    public void Clear()
    {
        item = null; 
        amount = 0;
    }
    //+ различные флаги (заблокирован, выделен и тд)
}
