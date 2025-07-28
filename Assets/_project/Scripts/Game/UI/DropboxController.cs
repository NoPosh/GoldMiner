using UnityEngine;
using UnityEngine.EventSystems;

public class DropboxController : MonoBehaviour, IDropHandler
{
    //По хорошему, чтобы он появлялся при переносе предмета
    public void OnDrop(PointerEventData eventData)
    {
        var slot = InventorySlotUI.draggedSlot;
        if (slot != null)
        {
            slot.InventoryComponent.DropItem(slot.slotIndex);
        }
    }
}
