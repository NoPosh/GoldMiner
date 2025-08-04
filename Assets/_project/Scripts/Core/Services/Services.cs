
using MyGame.Core;

public static class Services
{
    public static InventoryInteractionService InventoryInteractionService { get; private set;}
    public static CreatingNpcService CreatingNpcService { get; private set;}
    //+ ������ �������

    public static void Initialize(NpcDatabase npcDatabase)
    {
        InventoryInteractionService = new InventoryInteractionService();
        CreatingNpcService = new CreatingNpcService(npcDatabase);
    }
}
