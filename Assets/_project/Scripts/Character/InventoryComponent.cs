using UnityEngine;
using System.Collections.Generic;
using MyGame.EventBus;

public class InventoryComponent : MonoBehaviour
{
    [SerializeField] private int size = 20;
    public int Size {  get { return size; } }
    public List<InventoryCell> cells = new List<InventoryCell>();

    [SerializeField] private Transform dropPoint;
    [SerializeField] private float dropForce = 5f; //Мб сделать что-то типо мини игры с силой кидания

    private void Awake()
    {
        for (int i = 0; i < size; i++)
        {
            InventoryCell cell = new InventoryCell(i, this);
            cells.Add(cell);
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

    public void RemoveItem(int index, int amount = 1)
    {
        cells[index].Clear();
        EventBus.Raise<OnInventoryChanged>(new OnInventoryChanged());
    }
    
    //Перемещение внутри одного инвентаря
    public bool MoveItem(int fromIndex, int toIndex)
    {
        if (toIndex == -1) return false;
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
        if (fromIndex == -1 || toIndex == -1) return false;
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

    public void DropItem(int index, int amount = 1)
    {
        //Заспавнить с теми же скриптами (сейчас это либо WorldItem, либо Ore)
        GameObject dropped = Instantiate(cells[index].item.itemPrefab, dropPoint);
        dropped.transform.SetParent(null);
        dropped.GetComponent<WorldItem>().amount = cells[index].amount;

        RemoveItem(index);
       
        Rigidbody rb = dropped.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(dropPoint.forward * dropForce, ForceMode.Impulse);
        }
    }

    public void UseItem(int index)
    {

    }

    public BaseItem GetItem(int index)
    {
        return cells[index].item;
    }   //Надо ли

    public int GetFirstFreeSlotIndex()
    {
        int ind = -1;
        foreach (var cell in cells)
        {
            if (cell.item == null)
            {
                ind = cell.index;
                return ind;
            }
        }
        return ind;
    }
}

[System.Serializable]
public class InventoryCell  //Мб сюда добавить, чтобы знал номер слота
{
    public BaseItem item;
    public int amount;
    public int index = -1;
    public InventoryComponent Inventory;

    public bool IsEmpty => item == null || amount <= 0;

    public InventoryCell(int index, InventoryComponent inventory)
    {
        this.index = index;
        this.Inventory = inventory;
    }

    public void Clear()
    {
        item = null; 
        amount = 0;
    }
    //+ различные флаги (заблокирован, выделен и тд)
}
