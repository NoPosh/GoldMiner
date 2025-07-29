using System.Collections.Generic;
using UnityEngine;
using MyGame.EventBus;

public class InventoryUI : MonoBehaviour
{
    public InventoryComponent Inventory {  get; protected set; }
    [SerializeField] protected Transform gridParent;   
    [SerializeField] protected InventorySlotUI slotPrefab;

    protected List<InventorySlotUI> slots = new();

    public virtual void Bind(InventoryComponent inv)
    {
        Inventory = inv;
        BuildGrid();
        UpdateUI();
    }

    protected void OnEnable()
    {
        EventBus.Subscribe<OnInventoryChanged>(UpdateUI);
        UpdateUI();
    }

    protected void OnDisable()
    {
        EventBus.Unsubscribe<OnInventoryChanged>(UpdateUI);
    }

    protected virtual void BuildGrid()
    {
        foreach (var slot in slots)
        {
            Destroy(slot.gameObject);
        }
        slots.Clear();
        for (int i = 0; i < Inventory.Size; i++)
        {
            var slot = Instantiate(slotPrefab, gridParent);
            slot.SetSlotIndex(i);
            slot.SetInventory(Inventory);
            slots.Add(slot);
        }
    }


    //Если слоты будут меняться динамически, то можно добавить метод RebuildGrid, который добавляет, удаляет
    protected virtual void UpdateUI()
    {
        if (Inventory == null || slots.Count == 0) return;

        for (int i = 0; i < slots.Count; i++)
        {
            
            slots[i].Refresh();
        }

    } 
    
}
