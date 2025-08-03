using MyGame.Inventory;

public class InventoryInteractionService
{
    public bool TransferItem(InventoryComponent fromInv, int fromIndex, InventoryComponent toInv, int toIndex, InteractionContext context)
    {
        var fromCell = fromInv.Inventory.GetCell(fromIndex);

        if (context.IsDrop) //&& fromCell.item.IsQuestItem
        {
            //Нельзя выбросить квестовые предметы
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
        //Тут какая-нибудь логика стоимости и тд
        //Комиссия за торговлю, блокировка квестовых предметов, разрешения (NPC Только отдает и тд)
        return true;
    }

    //Можно много добавить, например
    //Можно ли забрать предмет из сундука
    //Списать золото при торговле
    //Обработка квестовых условийй (нельзя выкинуть квестовый предмет)
    //Что в перераб можно положить только руду 
}
