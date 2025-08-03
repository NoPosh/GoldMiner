using UnityEngine;
using MyGame.Core;
using MyGame.Core.Npc;

public class CharacterComponent : MonoBehaviour, ITrader
{
    public PlayerContext playerContext { get; private set; }
    public InventoryComponent InventoryComponent { get; private set; }

    private void Awake()
    {
        InventoryComponent = GetComponent<InventoryComponent>();
        if (InventoryComponent == null) Debug.LogWarning("��������� �� ��������");
        playerContext = new PlayerContext(InventoryComponent.Inventory);
    }

    public bool HasEnoughGold(int amount)
    {
        if (playerContext.Money.MoneyAmount >= amount) return true;
        return false;
    }
    public void DeductGold(int amount)
    {
        playerContext.Money.SpendMoney(amount);
        Debug.Log($"����� �������� {amount} �����");
    }
    public void AddGold(int amount)
    {
        playerContext.Money.AddMoney(amount);
        Debug.Log($"����� ������� {amount} �����");
    }
}
