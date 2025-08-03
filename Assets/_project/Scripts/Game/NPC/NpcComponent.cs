using MyGame.Core;
using MyGame.Core.Npc;
using MyGame.EventBus;
using MyGame.Inventory;
using UnityEngine;

public class NpcComponent : MonoBehaviour, IInteractable, ITrader
{
    public NpcData NpcData;
    [SerializeField] private int inventorySize;
    [SerializeField] private int monewAmount = 0;

    public NpcContext NpcContext {  get; private set; }

    private CharacterComponent characterComponent;
    public InventoryComponent InventoryComponent { get; private set; }

    private void Awake()
    {
        NpcContext = new NpcContext(NpcData, inventorySize, monewAmount);

        InventoryComponent = GetComponent<InventoryComponent>();
        if (InventoryComponent == null) InventoryComponent = gameObject.AddComponent<InventoryComponent>();
        InventoryComponent.Initialize(NpcContext.Inventory);

        NpcContext.OnDialogueStart += () => StartDialogue();
        
    }

    private void OnDisable()
    {
        NpcContext.OnDialogueStart -= () => StartDialogue();
    }

    public void Interact(GameObject interactor)
    {
        characterComponent = interactor.GetComponent<CharacterComponent>();
        this.NpcContext.Interact();
    }

    private void StartDialogue()    //Возможно надо уточнить какой диалог если будет Дерево
    {
        //Тут нужно что-то типо UI_DialogueManager
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
