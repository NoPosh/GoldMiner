using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class WorldItem : MonoBehaviour, IInteractable
{
    public BaseItem item;
    public int amount = 1;


    public void Interact(GameObject interactor)
    {
        //��� �������������� ���������� ������� (�������� OnPickupItem)
        //�������� ���� ������������ �������

        //������ EventBus.Raise(new ItemPickedUpEvent(player, item));
        //InventorySystem �������� � ��������� �������.
        //QuestSystem ���������, �� ������ �� ������� � �������.
        //UIManager ���������� �����������.
        //SoundManager ������ ����.
        //Analytics ���������� ����������.
        //����������� ������ � �������, ���������� � ��

        //����������� ������ -> ������ ������� �������� -> � ������� ����������, ��� ������ ������ -> ������� ��� ������ -> ���� � ui
        if (interactor.TryGetComponent(out InventoryComponent inventory))
        {
            // �������, ��� ������� ��������
            //EventBus.Raise(new ItemPickedUpEvent(interactor, item, amount));

            // ����� ����� ���������� ������
            Destroy(gameObject);
        }
    }
}
