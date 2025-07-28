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
        //������� ��������?
        //��������� ������ + ���������
        Debug.Log("�������������� � ��������");
        EventBus.Raise<OnInventoryInteract>(new OnInventoryInteract(interactor.GetComponent<InventoryComponent>(), inventoryComponent));
    }
}
