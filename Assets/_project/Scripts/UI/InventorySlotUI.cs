using UnityEngine;

public class InventorySlotUI : MonoBehaviour
{
    //Префаб слота - своя картинка + предмета
    private int slotIndex;
    private InventoryComponent inventoryComponent;

    public void SetSlotIndex(int index)
    {
        slotIndex = index;
    }

    public void SetInventory(InventoryComponent inventory)
    {
        inventoryComponent = inventory;
    }

    public void Refresh()
    {
        //Показать предмет в слоте
    }
}
