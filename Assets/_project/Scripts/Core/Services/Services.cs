using UnityEngine;

public static class Services
{
    public static InventoryInteractionService InventoryInteractionService { get; private set; }
    //+ ������ �������

    public static void Initialize()
    {
        InventoryInteractionService = new InventoryInteractionService();
        //� ������
    }

    //����� ����������� ����� ������
    //� GameManager.Awake ��� Bootstrapper Services.Initialize(), � ����� �����
    //Services.InventoryInteractions.TransferItem(playerInv, 0, chestInv, 3);
}
