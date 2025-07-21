using UnityEngine;

[CreateAssetMenu(menuName = "Items/BaseItem")]
public class BaseItem : ScriptableObject
{
    //Может быть помещен в инвентарь
    public string itemName;
    public string itemDiscription;

    public Sprite icon;
    public bool isStackable;
    public int maxStack = 1;
    public int price;
}
