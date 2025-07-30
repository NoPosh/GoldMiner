
using MyGame.Core.Npc;
using System.Collections;

public interface INpcService
{
    public void RegisterNpc(NpcContext context);
    void Tick(float deltaTime);
    //IEnumerable<NpcContext> GetAll();
}
