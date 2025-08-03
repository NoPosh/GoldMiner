using MyGame.Inventory;

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
            if (!(fromCell.item is OreData))
            {
                return false;
            }
        }


        if (context.IsTrade && PlayerHasEnoughGold(fromInv.GetItem(fromIndex)))
        {
            
        }
            


        return TransferItem(fromInv, fromIndex, toInv, toIndex);
    }

    public bool TransferItem(InventoryComponent fromInv, int fromIndex, InventoryComponent toInv, int toIndex)
    {
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
