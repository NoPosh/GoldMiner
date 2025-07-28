using UnityEngine;
using MyGame.Inventory;

[System.Serializable]
public class InventoryCell
{
    public BaseItem item;
    public int amount;
    public int index = -1;
    public Inventory Inventory;

    public bool IsEmpty => item == null || amount <= 0;

    public InventoryCell(int index, Inventory inventory)
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
