using System.Collections.Generic;
using UnityEngine;

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

    private void UpdateUI()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].Refresh(); // обновить иконку/текст
        }
    } 
    
}
