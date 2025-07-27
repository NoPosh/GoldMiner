using UnityEngine;

[CreateAssetMenu(menuName = "Game/OreData")]
public class OreData : BaseItem
{
    public Vector2Int potentialRange; //�������� (20, 50)
    //���?
    public GameObject orePrefab;
    public float artifactChance;
}
