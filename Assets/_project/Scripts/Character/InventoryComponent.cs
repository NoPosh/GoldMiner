using UnityEngine;
using System.Collections.Generic;

public class InventoryComponent : MonoBehaviour
{
    [SerializeField] private int size = 20;
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
                    if (amount <= 0) return true;
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
                return true;
            }
        }

        Debug.Log("Нет места в инвентаре");
        return false;    //Инвентарь заполнен
    }

    public void RemoveItem(BaseItem item)
    {
        //Трата ресурсов
    }
    //UseItem?
}

[System.Serializable]
public class InventoryCell          //Может быть AddItem сюда вшить, как и остальное
{
    public BaseItem item;
    public int amount;

    public bool IsEmpty => item == null || amount <= 0;
    //+ различные флаги (заблокирован, выделен и тд)
}
