
using MyGame.Core.Npc;
using System.Collections.Generic;

public interface IInteractionProvider
{
    IEnumerable<InteractionOption> GetAvailableInteractions(NpcContext context);
}
