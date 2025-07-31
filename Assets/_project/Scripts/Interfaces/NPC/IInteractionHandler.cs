
using MyGame.Core.Npc;

public interface IInteractionHandler
{
    bool CanHandle(string interactionId);
    void Handle(string interactionId, NpcContext context);
}
