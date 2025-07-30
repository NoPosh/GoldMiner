using MyGame.Core.Npc;
using UnityEngine;

public interface INpcCapability
{
    void Initialize(NpcContext context);
    void OnInteract();      //����� ���������: OnUpdate, OnPlayerNearby
}
