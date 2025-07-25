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
        EventBus.Raise<OnOpenChest>(new OnOpenChest(inventoryComponent));
    }
}
