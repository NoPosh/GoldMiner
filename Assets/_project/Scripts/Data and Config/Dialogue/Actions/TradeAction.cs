using MyGame.EventBus;
using UnityEngine;
[CreateAssetMenu(fileName = "TradeAction", menuName = "Dialogue/Actions/Trade")]
public class TradeAction : DialogueAction
{
    public override void Execute(DialogueContext context)
    {
        InteractionContext interactionContext = new InteractionContext {IsTrade = true };
        EventBus.Raise<OnInventoryInteract>(new OnInventoryInteract(context.characterComponent.Inventory, context.npcComponent.InventoryComponent, interactionContext));
        //Мне нужен такой трейд:
        //1. Открыть UI второго инвентаря
        //2. Если туда перенести золотые самородки, то выдадут деньги
        //3. 
    }
}
