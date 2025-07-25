using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDropHandler
{
    //Префаб слота - своя картинка + предмета
    private int slotIndex;
    private InventoryComponent inventoryComponent;

    public InventoryComponent Inventory {  get; private set; }

    private static InventorySlotUI draggedSlot;

    [SerializeField] Image itemImage;
    [SerializeField] Button Button;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            draggedSlot = this;
        else if (eventData.button == PointerEventData.InputButton.Right)
            ShowContextMenu();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (draggedSlot != null && draggedSlot != this)
            {
                // Один и тот же инвентарь
                if (draggedSlot.Inventory == Inventory)
                    Inventory.MoveItem(draggedSlot.slotIndex, slotIndex);
                else
                    InventoryComponent.MoveItemBetween(draggedSlot.Inventory, draggedSlot.slotIndex, Inventory, slotIndex);
            }

            draggedSlot = null;
        }
    }

    public void OnDrop (PointerEventData eventData)
    {

    }

    private void ShowContextMenu()
    {
        var item = inventoryComponent.cells[slotIndex].item;
        if (item != null)
        {
            // Вызов окна с вариантами: "Использовать", "Выбросить", и т.д.
            Debug.Log($"ПКМ меню для {item.itemName}");
        }
    }

    public void SetSlotIndex(int index)
    {
        slotIndex = index;
    }

    public void SetInventory(InventoryComponent inventory)
    {
        inventoryComponent = inventory;
    }

    private void OnClick()  //Клики будут ПКМ и ЛКМ, ЛКМ - перенос, ПКМ - меню действия с предметом
    {
        var item = inventoryComponent.cells[slotIndex].item;
        if (item != null)
        {
            Debug.Log($"Clicked on {item.name}");
            // Тут можно вызвать EventBus событие, показать popup и т.д.
        }
    }

    public void Refresh()
    {
        //Показать предмет в слоте
        var item = inventoryComponent.cells[slotIndex].item;
        if (item != null && item.icon != null)
        {
            itemImage.enabled = true;
            itemImage.sprite = item.icon;
        }
        else
        {
            itemImage.enabled = false;
        }
    }


}
