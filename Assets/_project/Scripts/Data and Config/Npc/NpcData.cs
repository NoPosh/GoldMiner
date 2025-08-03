using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NPC/NpcPreset")]
public class NpcData : ScriptableObject
{
    public string id;
    public string npcName;
    public Dialogue dialog;
   
}

public enum NpcType
{
    Simple, //�������, �������� ����� ������� �����
    Customer, //����� ������ � �������, ���������� ������ ��� �������
    Quest, //����� ������ �������, ����������� �� �������, �������
    Special //��� ����������������� + ���-�� ���
}
