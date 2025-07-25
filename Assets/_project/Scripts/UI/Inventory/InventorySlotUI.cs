using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    //������ ����� - ���� �������� + ��������
    private int slotIndex;
    private InventoryComponent inventoryComponent;

    public InventoryComponent Inventory { get { return inventoryComponent; } private set { } }

    private static InventorySlotUI draggedSlot;

    [SerializeField] Image itemImage;
    [SerializeField] Button Button;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (inventoryComponent.cells[slotIndex].item != null)
            {
                draggedSlot = this;
                SetAlpha(0.5f);
                DragIconController.Instance.ShowIcon(itemImage.sprite);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggedSlot == null) return;

        //���� ����� ������� ������� �� ������ (������� ��-�������, �� ���� ��� ����� ��������� � ����� �������)
    }

    public void OnEndDrag(PointerEventData eventData)
    {        
        draggedSlot = null;
        DragIconController.Instance.HideIcon();
        SetAlpha(1f);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (draggedSlot != null && draggedSlot != this)
        {
            if (draggedSlot.Inventory == Inventory)
                Inventory.MoveItem(draggedSlot.slotIndex, slotIndex);
            else
                InventoryComponent.MoveItemBetween(
                    draggedSlot.Inventory, draggedSlot.slotIndex,
                    Inventory, slotIndex
                );
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            ShowContextMenu();
    }

    private void ShowContextMenu()
    {
        var item = inventoryComponent.cells[slotIndex].item;
        if (item != null)
        {            
            // ����� ���� � ����������: "������������", "���������", � �.�.
            // �� ������ ��� ���������, ������� � ����� ���������?
            // ��� ��������� � ������� ��� ��� �������� ������ ����������� ��������?
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

    private void SetAlpha(float alpha)
    {
        var color = itemImage.color;
        color.a = alpha;
        itemImage.color = color;
    }
}
