using UnityEngine;

public abstract class NpcData : ScriptableObject
{
    public string npcName;
    public Sprite icon;
    public NpcType npcType;
    //public NpcBehaviorConfig behaviorConfig; //������ � ���������, �����������, ����������
}

public enum NpcType //��� ������ ���������, ����� ����� ������
{
    IdleWanderer,
    Trader,
    QuestGiver,
    Scavenger,
    GhostMiner
}