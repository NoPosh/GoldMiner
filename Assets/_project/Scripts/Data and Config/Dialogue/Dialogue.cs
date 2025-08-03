using MyGame.Core;
using MyGame.Core.Npc;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/DialogueAsset")]
public class Dialogue : ScriptableObject
{
    public DialogueNode startNode;
}

public class DialogueContext
{
    public CharacterComponent characterComponent;
    public NpcComponent npcComponent;

    public DialogueContext(CharacterComponent player, NpcComponent npc)
    {
        characterComponent = player;
        npcComponent = npc;
    }
}

