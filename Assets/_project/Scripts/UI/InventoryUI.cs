using System.Collections.Generic;
using UnityEngine;
using MyGame.EventBus;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventoryComponent inventory;
    [SerializeField] private Transform gridParent;   
    [SerializeField] private InventorySlotUI slotPrefab;

    private List<InventorySlotUI> slots = new();

    private void Start()
    {
        BuildGrid();
    }

    private void OnEnable()
    {
        EventBus.Subscribe<OnInventoryChanged>(UpdateUI);
        UpdateUI();
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnInventoryChanged>(UpdateUI);
    }

    private void BuildGrid()
    {
        for (int i = 0; i < inventory.Size; i++)
        {
            var slot = Instantiate(slotPrefab, gridParent);
            slot.SetSlotIndex(i);
            slot.SetInventory(inventory);
            slots.Add(slot);
        }
    }

    //Если слоты будут меняться динамически, то можно добавить метод RebuildGrid, который добавляет, удаляет

    private void UpdateUI()
    {
        if (inventory == null || slots.Count == 0) return;

        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].Refresh();
        }
    } 
    
}
