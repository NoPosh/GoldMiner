using System.Collections.Generic;
using UnityEngine;

public class RecycleInventoryUI : InventoryUI
{
    [SerializeField] protected Transform outputGridParent;
    public InventoryComponent OutputInventory { get; private set; }

    private List<InventorySlotUI> outputSlots = new();
    public RecyclerComponent Recycler { get; private set; }
    public void Bind(RecyclerComponent recycle, IInteractionContext context)
    {
        Recycler = recycle;
        Inventory = recycle.InputInventory;
        OutputInventory = recycle.OutputInventory;

        BuildGrid(context);
        UpdateUI();
    }

    protected override void BuildGrid(IInteractionContext context)
    {
        base.BuildGrid(context);

        foreach (var slot in outputSlots)
        {
            Destroy(slot.gameObject);
        }
        outputSlots.Clear();
        for (int i = 0; i < OutputInventory.Size; i++)
        {
            var slot = Instantiate(slotPrefab, outputGridParent);
            slot.SetSlotIndex(i);
            slot.SetInventory(OutputInventory);
            slot.context = context;
            outputSlots.Add(slot);
        }

    }

    protected override void UpdateUI()
    {
        base.UpdateUI();
        if (OutputInventory == null || outputSlots.Count == 0) return;

        for (int i = 0; i < outputSlots.Count; i++)
        {

            outputSlots[i].Refresh();
        }

    }
}
