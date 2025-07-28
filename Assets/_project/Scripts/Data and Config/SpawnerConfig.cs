using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Game/SpawnerConfig")]
public class SpawnerConfig : ScriptableObject
{
    public enum SpawnZone
    {
        Simple,
        River
    }
    [Tooltip("������ �� ������")] public SpawnZone spawnZone;
    public List<OreData> possibleOres;
    public List<float> spawnChances;
    public int MaxObjects = 10;
    public float SpawnInterval = 15f;
    //����, �������� ����
    //������ ��������� "OreData" � �������
    //�������� ��������
    //�������� ������
}
