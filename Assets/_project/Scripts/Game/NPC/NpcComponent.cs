using MyGame.Core;
using MyGame.Core.Npc;
using MyGame.EventBus;
using MyGame.Inventory;
using System;
using UnityEngine;

public class NpcComponent : MonoBehaviour, IInteractable, ITrader
{
    [Header("Настройка НПС")]
    public NpcData NpcData;
    [SerializeField] private int inventorySize;
    [SerializeField] private int moneyAmount = 0;

    public NpcContext NpcContext {  get; private set; }

    private CharacterComponent characterComponent;
    public InventoryComponent InventoryComponent { get; private set; }

    private Action handler;

    private void Awake()
    {
        handler = () => StartDialogue();

        NpcContext = new NpcContext(NpcData, inventorySize, moneyAmount);
        Init(NpcContext);                   
    }

    public void Init(NpcContext context)
    {
        NpcContext.OnDialogueStart -= handler;

        NpcData = context.Data;
        NpcContext = context;
        inventorySize = NpcContext.Inventory.Size;
        moneyAmount = NpcContext.Money.MoneyAmount;

        InventoryComponent = GetComponent<InventoryComponent>();
        if (InventoryComponent == null) InventoryComponent = gameObject.AddComponent<InventoryComponent>();
        InventoryComponent.Initialize(NpcContext.Inventory);
       
        NpcContext.OnDialogueStart += handler;
    }


    private void OnDisable()
    {
        NpcContext.OnDialogueStart -= () => StartDialogue();
    }

    public void Interact(GameObject interactor)
    {
        characterComponent = interactor.GetComponent<CharacterComponent>();
        Debug.Log(NpcContext.CanInteract);
        this.NpcContext.Interact();
    }

    private void StartDialogue()    //Возможно надо уточнить какой диалог если будет Дерево
    {
        //Тут нужно что-то типо UI_DialogueManager
        Debug.Log("Начали диалог");
        EventBus.Raise<OnDialogieStart>(new OnDialogieStart(NpcData.dialog, new DialogueContext(characterComponent, this)));
    }

    public bool HasEnoughGold(int amount)
    {
        if (NpcContext.Money.MoneyAmount >= amount) return true;
        return false;
    }
    public void DeductGold(int amount)
    {
        NpcContext.Money.SpendMoney(amount);
    }
    public void AddGold(int amount)
    {
        NpcContext.Money.AddMoney(amount);
    }
}
