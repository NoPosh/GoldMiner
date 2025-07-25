using MyGame.EventBus;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    private InventoryComponent inventoryComponent;

    public void Start()
    {
        inventoryComponent = GetComponent<InventoryComponent>();
    }

    public void Interact(GameObject interactor)
    {
        //Спавнит предметы?
        //Открывает сундук + инвентарь
        Debug.Log("Взаимодействие с сундуком");
        EventBus.Raise<OnOpenChest>(new OnOpenChest(inventoryComponent));
    }
}
