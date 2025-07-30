using UnityEngine;

public abstract class NpcData : ScriptableObject
{
    public string npcName;
    public Sprite icon;
    public NpcType npcType;
    //public NpcBehaviorConfig behaviorConfig; //ƒанные о характере, склонност€х, расписании
}

public enum NpcType //ƒл€ начала рандомные, потом будут другие
{
    IdleWanderer,
    Trader,
    QuestGiver,
    Scavenger,
    GhostMiner
}