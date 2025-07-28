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
    public int maxObjects = 10;
    public float spawnInterval = 15f;
    //����, �������� ����
    //������ ��������� "OreData" � �������
    //�������� ��������
    //�������� ������
}
