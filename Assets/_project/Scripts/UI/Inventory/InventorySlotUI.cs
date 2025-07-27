using MyGame.EventBus;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    //������ ����� - ���� �������� + ��������
    public int slotIndex { get; private set; }
    private InventoryComponent inventoryComponent;

    private InputAction shiftAction;

    public InventoryComponent Inventory { get { return inventoryComponent; } private set { } }

    public static InventorySlotUI draggedSlot { get; private set; }
    [SerializeField] Image itemImage;
    [SerializeField] Button Button;

    private void Awake()
    {
        shiftAction = InputSystem.actions.FindAction("Sprint");
    }

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

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventBus.Raise(new OnItemPointerEnter(inventoryComponent.cells[slotIndex]));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EventBus.Raise(new OnItemPointerExit());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            ShowContextMenu();

        if (eventData.button == PointerEventData.InputButton.Left && shiftAction.IsPressed())
        {
            //�����, ������� �������� Cell
            EventBus.Raise<OnItemShiftClick>(new OnItemShiftClick(inventoryComponent.cells[slotIndex]));
        }
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
        if (itemImage == null)
        {
            Debug.Log("��� Image ��� " + inventoryComponent);
            return;
        }
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
