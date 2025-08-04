using UnityEngine;
using MyGame.Core;

public class ShowcaseComponent : MonoBehaviour, IInteractable
{
    [SerializeField] private int inventorySize = 1;
    public Showcase Showcase { get; private set;}

    public InventoryComponent Inventory { get; private set; }

    private void Awake()
    {
        Showcase = new Showcase(inventorySize);
        Inventory = GetComponent<InventoryComponent>();
        if (Inventory == null)
        {
            Inventory = gameObject.AddComponent<InventoryComponent>();
        }
        Inventory.Initialize(Showcase.Inventory);
    }

    public void Interact(GameObject interactor)
    {
        //��������� UI (����� ����) - ��� ���� ������, ��� ���� ����� � ������ �����������
        //��� ���� � ������ ������ ��� ��� ��������

        //���� ������� ����, �� ��� ����� �������, ����� ������ ���������� �����
    }
}
