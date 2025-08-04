using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NpcDatabase", menuName = "Npc/NpcDatabase")]
public class NpcDatabase : ScriptableObject
{
    public List<NpcData> allNpcs;

    public List<NpcData> customersNpc;

    public NpcData GetRandomCustomer()
    {
        return customersNpc[Random.Range(0, customersNpc.Count)];
    }
}
