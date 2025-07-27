using UnityEngine;
using UnityEngine.EventSystems;

public class DropboxController : MonoBehaviour, IDropHandler
{
    //По хорошему, чтобы он появлялся при переносе предмета
    public void OnDrop(PointerEventData eventData)
    {
        var slot = InventorySlotUI.draggedSlot;
        //Если отпустили тут, то дропаем ту ячейку, которую перенесли
        if (slot != null)
        {
            slot.Inventory.DropItem(slot.slotIndex);
        }
    }
}
