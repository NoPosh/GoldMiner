using UnityEngine;

public static class Services
{
    public static InventoryInteractionService InventoryInteractionService { get; private set; }
    //+ другие сервисы

    public static void Initialize()
    {
        InventoryInteractionService = new InventoryInteractionService();
        //И другие
    }

    //Когда понадобится можно просто
    //В GameManager.Awake или Bootstrapper Services.Initialize(), а потом везде
    //Services.InventoryInteractions.TransferItem(playerInv, 0, chestInv, 3);
}
