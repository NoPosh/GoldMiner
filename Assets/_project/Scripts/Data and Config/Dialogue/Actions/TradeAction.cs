using MyGame.EventBus;
using UnityEngine;
[CreateAssetMenu(fileName = "TradeAction", menuName = "Dialogue/Actions/Trade")]
public class TradeAction : DialogueAction
{
    public override void Execute(DialogueContext context)
    {
        TradeContext interactionContext = new TradeContext(context.characterComponent, context.npcComponent);
        EventBus.Raise<OnInventoryInteract>(new OnInventoryInteract(context.characterComponent.InventoryComponent, context.npcComponent.InventoryComponent, interactionContext));
    }
}
