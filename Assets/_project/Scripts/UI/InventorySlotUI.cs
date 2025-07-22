using UnityEngine;

public class InventorySlotUI : MonoBehaviour
{
    //������ ����� - ���� �������� + ��������
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
        //�������� ������� � �����
    }
}
