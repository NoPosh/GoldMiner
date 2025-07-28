using System.Collections.Generic;
using MyGame.EventBus;
using UnityEngine;

public class UI_RecycleInventory : MonoBehaviour
{
    [SerializeField] private GameObject RecyclePanel;
    [SerializeField] private Transform gridParent;
    [SerializeField] private Transform secondGridParent;

    [SerializeField] private InventorySlotUI slotPrefab;

    private Recycler currentRecycler;
    [SerializeField] private List<InventorySlotUI> slots = new();

    [SerializeField] private List<InventorySlotUI> secondSlots = new();

    private void OnEnable() //TODO: закрытие этой панели
    {
        EventBus.Subscribe<OnOpenRecycle>(OpenRecycleUI);
        EventBus.Subscribe<OnInventoryChanged>(UpdateUI);
        EventBus.Subscribe<OnInventoryClose>(CloseRecycleUI);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnOpenRecycle>(OpenRecycleUI);
        EventBus.Unsubscribe<OnInventoryChanged>(UpdateUI);
        EventBus.Unsubscribe<OnInventoryClose>(CloseRecycleUI);
    }

    private void Start()
    {
        RecyclePanel.SetActive(false);
    }

    private void OpenRecycleUI(OnOpenRecycle e)
    {
        currentRecycler = e.recycler;

        RecyclePanel.SetActive(true);
        BuildGrid();
        UpdateUI();
    }

    private void CloseRecycleUI()
    {
        RecyclePanel.SetActive(false);
    }

    private void BuildGrid()
    {
        foreach (Transform tr in gridParent)
        {
            Destroy(tr.gameObject);
        }
        foreach (Transform tr in secondGridParent)
        {
            Destroy(tr.gameObject);
        }
        slots.Clear();
        secondSlots.Clear();

        for (int i = 0; i < currentRecycler.InputInventory.Size; i++)
        {
            var slot = Instantiate(slotPrefab, gridParent);
            slot.SetSlotIndex(i);
            slot.SetInventory(currentRecycler.InputInventory);
            slots.Add(slot);
        }

        for (int i = 0; i < currentRecycler.OutputInventory.Size; i++)
        {
            var slot = Instantiate(slotPrefab, secondGridParent);
            slot.SetSlotIndex(i);
            slot.SetInventory(currentRecycler.OutputInventory);
            secondSlots.Add(slot);
        }
    }

    private void UpdateUI()
    {
        if (currentRecycler == null) return;

        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].Refresh();
        }
        for (int i = 0; i < secondSlots.Count; i++)
        {
            secondSlots[i].Refresh();
        }
    }
}
