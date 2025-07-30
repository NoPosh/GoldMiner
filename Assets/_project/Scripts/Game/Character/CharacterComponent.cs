using UnityEngine;
using MyGame.Core;

public class CharacterComponent : MonoBehaviour
{
    public PlayerContext playerContext { get; private set; }
    public InventoryComponent Inventory { get; private set; }

    private void Awake()
    {
        Inventory = GetComponent<InventoryComponent>();
        if (Inventory == null) Debug.LogWarning("Инвентарь не назначен");
        playerContext = new PlayerContext(Inventory.Inventory);
    }
}
