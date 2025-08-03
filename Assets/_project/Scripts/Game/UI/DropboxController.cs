using UnityEngine;
using UnityEngine.EventSystems;

public class DropboxController : MonoBehaviour, IDropHandler
{
    //�� ��������, ����� �� ��������� ��� �������� ��������
    public void OnDrop(PointerEventData eventData)
    {
        var slot = InventorySlotUI.draggedSlot;
        if (slot != null)   //��� ���� ������� �������� ����� �� (�� ������ ����� ����������� �� ��������� ��������)
        {
            if (!(InventorySlotUI.draggedSlot.context is TradeContext))
                slot.InventoryComponent.DropItem(slot.slotIndex);
        }
    }
}
