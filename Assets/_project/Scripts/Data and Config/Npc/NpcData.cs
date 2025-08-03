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
    Simple, //Обычный, максимум может сказать фразу
    Customer, //Может прийти в ломбард, предложить купить или продать
    Quest, //Может выдать задание, реагировать на процесс, реплики
    Special //Все вышеперечисленное + что-то еще
}
