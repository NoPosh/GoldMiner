using UnityEngine;

[CreateAssetMenu(menuName = "Game/OreData")]
public class OreData : BaseItem
{
    public Vector2Int potentialRange; //например (20, 50)
    //Вес?
    public float artifactChance;
}
