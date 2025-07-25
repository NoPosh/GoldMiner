using UnityEngine;
using UnityEngine.Analytics;
using MyGame.EventBus;

public class WorldItem : MonoBehaviour, IInteractable
{
    public BaseItem item;
    public int amount = 1;


    public virtual void Interact(GameObject interactor)
    {
        //При взаимодействии вызывается событие (например OnPickupItem)
        //Менеджер игры обрабатывает событие

        //Просто EventBus.Raise(new ItemPickedUpEvent(player, item));
        //InventorySystem подписан и добавляет предмет.
        //QuestSystem проверяет, не связан ли предмет с квестом.
        //UIManager показывает уведомление.
        //SoundManager играет звук.
        //Analytics записывает статистику.
        //Добавляется запись в блокнот, сохранение и тд

        if (interactor.TryGetComponent(out InventoryComponent inventory))   //Возможно лучше InventorySystem
        {
            // Событие, что предмет хотят поднять с коллбеком

            EventBus.Raise(new ItemPickupAttemptEvent()
            {
                picker = interactor,
                item = this.item,
                amount = this.amount,
                onResult = (success) =>
                {
                    if (success) Destroy(gameObject);
                    else
                        Debug.Log("Не получилось взять этот предмет");
                }
            });
        }
    }

}
