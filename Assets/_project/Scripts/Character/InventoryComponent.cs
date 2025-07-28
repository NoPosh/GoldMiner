using UnityEngine;
using System.Collections.Generic;
using MyGame.EventBus;
using MyGame.Inventory;

public class InventoryComponent : MonoBehaviour
{
    [SerializeField] protected int size = 20;
    [SerializeField] protected Transform dropPoint;
    [SerializeField] protected float dropForce = 5f; //�� ������� ���-�� ���� ���� ���� � ����� �������

    public int Size => size;
    private Inventory inventory;
    public Inventory Inventory { get { return inventory; } }




    protected virtual void Awake()
    {
        inventory = new Inventory(size);
        inventory.OnInventoryChanged += () => EventBus.Raise(new OnInventoryChanged());
    }

    public bool AddItem(BaseItem newItem, int amount = 1)
    {

        return inventory.AddItem(newItem, amount);
    }

    public virtual void RemoveItem(int index, int amount = 1)
    {
        inventory.RemoveItem(index, amount);
    }
    
    //����������� ������ ������ ���������
    public virtual bool MoveItem(int fromIndex, int toIndex)
    {
        return inventory.MoveItem(fromIndex, toIndex);
    }

    //����������� ����� �����������
    public bool MoveItemBetween(InventoryComponent toInv, int fromIndex, int toIndex)
    {
        return Inventory.MoveItemBetween(inventory, fromIndex, toInv.inventory, toIndex);
    }

    public virtual void DropItem(int index, int amount = 1) //�������� � ���, ����� ������������ ���� ��������
    {
        var cell = inventory.GetCell(index);

        if (cell.IsEmpty) return;

        var prefab = cell.item.itemPrefab;

        var dropped = Instantiate(prefab, dropPoint.position, Quaternion.identity);   //�������� ������ �� ������
        dropped.GetComponent<WorldItem>().amount = cell.amount;

        RemoveItem(index);
       
        if (dropped.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.AddForce(dropPoint.forward * dropForce, ForceMode.Impulse);
        }
    }

    public virtual void UseItem(int index)
    {

    }

    public virtual BaseItem GetItem(int index)
    {
        return inventory.GetCell(index).item;
    }   //���� ��

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


