using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDropHandler
{
    //������ ����� - ���� �������� + ��������
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
                // ���� � ��� �� ���������
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
            // ����� ���� � ����������: "������������", "���������", � �.�.
            Debug.Log($"��� ���� ��� {item.itemName}");
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

    private void OnClick()  //����� ����� ��� � ���, ��� - �������, ��� - ���� �������� � ���������
    {
        var item = inventoryComponent.cells[slotIndex].item;
        if (item != null)
        {
            Debug.Log($"Clicked on {item.name}");
            // ��� ����� ������� EventBus �������, �������� popup � �.�.
        }
    }

    public void Refresh()
    {
        //�������� ������� � �����
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
