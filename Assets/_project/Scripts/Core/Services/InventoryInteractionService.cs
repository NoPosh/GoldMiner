using MyGame.Inventory;
using UnityEngine;

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
            //Тут проверка, что положить можно только породу
        }


        if (context.IsTrade && !PlayerHasEnoughGold(fromCell.item))
            return false;
        //Проверить тот же это инвентарь или нет
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
