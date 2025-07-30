using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NPC/NpcPreset")]
public class NpcData : ScriptableObject
{
    public string id;
    public string npcName;
    //��� ��� (Trader, QuestGiver, Enemy � ��)
    public List<NpcCapabilitySO> capabilities;  //��� �������� ���������� ����� ������������� - ������� ������ 

    
    //����� ����������� ��� NPC (�������� �� ������� � ������ ���������)
    /*
    ����� ����� ���� ����������?
    1. ����������� ����� ������
    2. ����������� ������ (� �������������?)
    3. ������ ������� (������� ������, ����������, �������)
     */
}
