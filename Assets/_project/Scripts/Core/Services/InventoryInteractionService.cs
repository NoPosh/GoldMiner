using MyGame.Inventory;
using UnityEngine;

public class InventoryInteractionService
{
    public bool TransferItem(InventoryComponent fromInv, int fromIndex, InventoryComponent toInv, int toIndex, InteractionContext context)
    {
        var fromCell = fromInv.Inventory.GetCell(fromIndex);

        if (context.IsDrop) //&& fromCell.item.IsQuestItem
        {
            //������ ��������� ��������� ��������
        }

        if (context.IsRecycler)
        {
            //��� ��������, ��� �������� ����� ������ ������
        }


        if (context.IsTrade && !PlayerHasEnoughGold(fromCell.item))
            return false;
        //��������� ��� �� ��� ��������� ��� ���
        if (!(fromInv == toInv))
            return Inventory.MoveItemBetween(fromInv.Inventory, fromIndex, toInv.Inventory, toIndex);
        else
        {
            return fromInv.MoveItem(fromIndex, toIndex);
        }
    }

    private bool PlayerHasEnoughGold(BaseItem item)
    {
        //��� �����-������ ������ ��������� � ��
        //�������� �� ��������, ���������� ��������� ���������, ���������� (NPC ������ ������ � ��)
        return true;
    }

    //����� ����� ��������, ��������
    //����� �� ������� ������� �� �������
    //������� ������ ��� ��������
    //��������� ��������� �������� (������ �������� ��������� �������)
    //��� � ������� ����� �������� ������ ���� 
}
