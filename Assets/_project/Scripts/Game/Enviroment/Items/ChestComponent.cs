using UnityEngine;
using MyGame.Core;
using MyGame.EventBus;

public class ChestComponent : MonoBehaviour, IInteractable
{
    [SerializeField] private int size = 6;
    [SerializeField] private string chestId;
    [SerializeField] private bool respawnLoot;

    public Chest Chest {  get; private set; }

    private void Awake()
    {
        Chest = new Chest(chestId, size, respawnLoot);
    }

    public void Interact(GameObject interactor)
    {
        EventBus.Raise<OnInventoryInteract>(new OnInventoryInteract(interactor.GetComponent<CharacterComponent>().InventoryComponent, GetInventoryComponent()));
    }

    public InventoryComponent GetInventoryComponent()
    {
        var component = gameObject.GetComponent<InventoryComponent>();
        if (component == null)
        {
            component = gameObject.AddComponent<InventoryComponent>();
            
        }
        component.Initialize(Chest.Inventory);
        return component;
    }
}
