using UnityEngine;
using UnityEngine.Analytics;
using MyGame.EventBus;

public class WorldItem : MonoBehaviour, IInteractable
{
    public BaseItem item;
    public int amount = 1;


    public virtual void Interact(GameObject interactor)
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

        if (interactor.TryGetComponent(out InventoryComponent inventory))   //�������� ����� InventorySystem
        {
            // �������, ��� ������� ����� ������� � ���������

            EventBus.Raise(new ItemPickupAttemptEvent()
            {
                picker = interactor,
                item = this.item,
                amount = this.amount,
                onResult = (success) =>
                {
                    if (success) Destroy(gameObject);
                    else
                        Debug.Log("�� ���������� ����� ���� �������");
                }
            });
        }
    }

}
