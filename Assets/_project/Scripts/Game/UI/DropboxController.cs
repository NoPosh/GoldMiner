using UnityEngine;
using UnityEngine.EventSystems;

public class DropboxController : MonoBehaviour, IDropHandler
{
    //По хорошему, чтобы он появлялся при переносе предмета
    public void OnDrop(PointerEventData eventData)
    {
        var slot = InventorySlotUI.draggedSlot;
        if (slot != null)   //Тут надо сделать проверку можно ли (тк сейчас можно выбрасывать из инвентаря торговца)
        {
            if (!(InventorySlotUI.draggedSlot.context is TradeContext))
                slot.InventoryComponent.DropItem(slot.slotIndex);
        }
    }
}
