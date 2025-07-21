using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class WorldItem : MonoBehaviour, IInteractable
{
    public BaseItem item;
    public int amount = 1;


    public void Interact(GameObject interactor)
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

        //Открывается сундук -> сундук спавнит предметы -> в квсетах отмечается, что сундук найден -> ловушка или засада -> звук и ui
        if (interactor.TryGetComponent(out InventoryComponent inventory))
        {
            // Событие, что предмет подобран
            //EventBus.Raise(new ItemPickedUpEvent(interactor, item, amount));

            // Можно сразу уничтожить объект
            Destroy(gameObject);
        }
    }
}
