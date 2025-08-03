
using MyGame.Inventory;
using System.Diagnostics;

public class TradeContext : IInteractionContext
{
    public bool IsDrop => false;
    public bool IsRecycler => false;
    public bool IsTrade => true;

    public ITrader Player { get; }
    public ITrader Other { get; }

    public TradeContext(ITrader player, ITrader other)  //По хорошему нам тут надо указать, кто покупает, кто продает, но как?
    {
        Player = player;
        Other = other;
    }
}


public interface ITrader
{
    bool HasEnoughGold(int amount);
    void DeductGold(int amount);
    void AddGold(int amount);
    InventoryComponent InventoryComponent { get; }
}