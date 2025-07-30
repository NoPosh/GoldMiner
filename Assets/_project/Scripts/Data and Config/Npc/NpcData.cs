using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NPC/NpcPreset")]
public class NpcData : ScriptableObject
{
    public string id;
    public string npcName;
    //Тип нпс (Trader, QuestGiver, Enemy и тд)
    public List<NpcCapabilitySO> capabilities;  //Все активные компоненты могут отреагировать - сначала диалог 

    
    //Набор компонентов для NPC (хотелось бы вынести в тонкую настройку)
    /*
    Какие могут быть компоненты?
    1. Возможность вести диалог
    2. Возможность трейда (с особенностями?)
    3. Выдача квестов (условия выдачи, завершения, провала)
     */
}
