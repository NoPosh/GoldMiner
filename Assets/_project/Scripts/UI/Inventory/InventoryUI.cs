using System.Collections.Generic;
using UnityEngine;
using MyGame.EventBus;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventoryComponent inventory;
    [SerializeField] private Transform gridParent;   
    [SerializeField] private InventorySlotUI slotPrefab;

    
    [SerializeField] private Transform sideGridParent;

    private List<InventorySlotUI> slots = new();

    private List<InventorySlotUI> sideSlots = new();

    private void Start()
    {
        BuildGrid();        
    }

    private void OnEnable()
    {
        EventBus.Subscribe<OnInventoryChanged>(UpdateUI);
        EventBus.Subscribe<OnOpenChest>(BuildSideGrid);
        UpdateUI();
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnInventoryChanged>(UpdateUI);
        EventBus.Unsubscribe<OnOpenChest>(BuildSideGrid);
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

    private void BuildSideGrid(OnOpenChest e)
    {
        foreach (Transform tr in sideGridParent)
        {
            Destroy(tr.gameObject);
        }
        sideSlots.Clear();

        for (int i = 0; i < e.Inventory.Size; i++)
        {
            var slot = Instantiate(slotPrefab, sideGridParent);
            slot.SetSlotIndex(i);
            slot.SetInventory(e.Inventory);
            sideSlots.Add(slot);
        }
        UpdateUI();
    }

    //Если слоты будут меняться динамически, то можно добавить метод RebuildGrid, который добавляет, удаляет

    private void UpdateUI()
    {
        if (inventory == null || slots.Count == 0) return;

        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].Refresh();
        }

        if (true)   //TODO: проверку надо ли обновлять слоты второго инвентаря
        {
            for (int i = 0; i < sideSlots.Count; i++)
            {
                sideSlots[i].Refresh();
            }
        }
    } 
    
}
