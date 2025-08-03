using MyGame.Inventory;

public class InventoryInteractionService
{
    public bool TransferItem(InventoryComponent fromInv, int fromIndex, InventoryComponent toInv, int toIndex, IInteractionContext context)
    {
        var fromCell = fromInv.Inventory.GetCell(fromIndex);
        if (fromInv == toInv) return TransferItem(fromInv, fromIndex, toInv, toIndex);

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


        if (context is TradeContext tradeContext)
        {
            int price = fromCell.item.itemPrice * fromCell.amount;
            if (tradeContext.Player.InventoryComponent == fromInv)
            {
                if (tradeContext.Other.HasEnoughGold(price))
                {
                    if (TransferItem(fromInv, fromIndex, toInv, toIndex))
                    {
                        tradeContext.Other.DeductGold(price);
                        tradeContext.Player.AddGold(price);
                        return true;
                    }
                }
            }
            else if (tradeContext.Other.InventoryComponent == fromInv)
            {
                if (tradeContext.Player.HasEnoughGold(price))
                {
                    if (TransferItem(fromInv, fromIndex, toInv, toIndex))
                    {
                        tradeContext.Player.DeductGold(price);
                        tradeContext.Other.AddGold(price);
                        return true;
                    }
                }
            }
            else return false;
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

    private bool PlayerHasEnoughGold(BaseItem item) //Тут нужно взять контекст второго персонажа и понять хватает ли у него денег заплатить
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
