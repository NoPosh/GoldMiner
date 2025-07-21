using UnityEngine;

public interface ICollectable
{
    bool TryCollect(InventoryComponent inventory);
}
