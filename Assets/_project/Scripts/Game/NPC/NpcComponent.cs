using MyGame.Core;
using MyGame.Core.Npc;
using MyGame.EventBus;
using MyGame.Inventory;
using UnityEngine;

public class NpcComponent : MonoBehaviour, IInteractable
{
    public NpcData NpcData;
    [SerializeField] private int inventorySize;

    public NpcContext NpcContext {  get; private set; }

    private CharacterComponent characterComponent;
    public InventoryComponent InventoryComponent { get; private set; }

    private void Awake()
    {
        NpcContext = new NpcContext(NpcData, inventorySize);

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
}
