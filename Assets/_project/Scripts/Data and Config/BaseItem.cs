using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/BaseItem")]
public class BaseItem : ScriptableObject, IPotentially
{
    public enum ItemType
    {
        None,
        Ore
    }

    //Может быть помещен в инвентарь
    public string itemName;
    public string itemDiscription;

    public ItemType itemType;
    public GameObject itemPrefab;
    public Sprite icon;
    public bool isStackable;
    public int maxStack = 1;

    public Vector2Int potentialRange = new Vector2Int(0, 0); //например (20, 50)
    public Vector2Int artifactPotentialRange = new Vector2Int(0, 0);

    public int GetPotential()
    {
        return Random.Range(potentialRange.x, potentialRange.y);
    }
    public int GetArtifactPotential()
    {
        return Random.Range(artifactPotentialRange.x, artifactPotentialRange.y);
    }
    public virtual void Use()   //Нужен не для всех
    {
        //Debug.Log("Предмет использован");
    }
}
