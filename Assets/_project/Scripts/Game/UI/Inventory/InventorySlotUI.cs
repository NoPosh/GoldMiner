using MyGame.EventBus;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public int slotIndex { get; private set; }
    public InventoryComponent InventoryComponent { get; private set; }

    private InputAction shiftAction;

    public IInteractionContext context;

    public static InventorySlotUI draggedSlot { get; private set; }
    [SerializeField] Image itemImage;
    [SerializeField] Button Button;
    [SerializeField] TMP_Text AmountText;

    private void Awake()
    {
        shiftAction = InputSystem.actions.FindAction("Sprint");
        if (context == null)
            context = new InteractionContext();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (InventoryComponent.Inventory.GetCell(slotIndex).item != null)
            {
                draggedSlot = this;
                SetAlpha(0.5f);
                DragIconController.Instance.ShowIcon(itemImage.sprite);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggedSlot == null) return;

        //Пока тянем предмет тянется за мышкой (сделано по-другому, но если что можно вернуться к этому способу)
    }

    public void OnEndDrag(PointerEventData eventData)
    {        
        draggedSlot = null;
        DragIconController.Instance.HideIcon();
        SetAlpha(1f);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (draggedSlot != null && draggedSlot != this)
        {
            Services.InventoryInteractionService.TransferItem(draggedSlot.InventoryComponent, draggedSlot.slotIndex, this.InventoryComponent, this.slotIndex, context);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventBus.Raise(new OnItemPointerEnter(InventoryComponent.Inventory.GetCell(slotIndex)));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EventBus.Raise(new OnItemPointerExit());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            ShowContextMenu();

        if (eventData.button == PointerEventData.InputButton.Left && shiftAction.IsPressed())
        {
            //Ивент, который передает Cell
            EventBus.Raise<OnItemShiftClick>(new OnItemShiftClick(InventoryComponent.Inventory.GetCell(slotIndex)));
        }
    }

    private void ShowContextMenu()
    {
        var item = InventoryComponent.Inventory.GetCell(slotIndex).item;
        if (item != null)
        {            
            // Вызов окна с вариантами: "Использовать", "Выбросить", и т.д.
            // Но только для предметов, которые в твоем инвентаре?
            // Для предметов в сундуке или при торговле другие контекстные действия?
            Debug.Log($"ПКМ меню для {item.itemName}");
        }
    }

    public void SetSlotIndex(int index)
    {
        slotIndex = index;
    }

    public void SetInventory(InventoryComponent inventory)
    {
        InventoryComponent = inventory;
    }

    public void Refresh()
    {
        //Показать предмет в слоте
        if (itemImage == null)
        {
            Debug.Log("Нет Image для " + InventoryComponent);
            return;
        }
        var item = InventoryComponent.Inventory.GetCell(slotIndex).item;
        if (item != null && item.icon != null)
        {
            itemImage.enabled = true;
            itemImage.sprite = item.icon;
            AmountText.enabled = true;
            AmountText.text = InventoryComponent.Inventory.GetCell(slotIndex).amount.ToString();
        }
        else
        {
            itemImage.enabled = false;
            AmountText.enabled = false;
        }
    }

    private void SetAlpha(float alpha)
    {
        var color = itemImage.color;
        color.a = alpha;
        itemImage.color = color;
    }
}
