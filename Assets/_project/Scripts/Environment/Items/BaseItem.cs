using UnityEngine;

[CreateAssetMenu(menuName = "Items/BaseItem")]
public class BaseItem : ScriptableObject
{
    //Может быть помещен в инвентарь
    public string itemName;
    public string itemDiscription;

    public GameObject itemPrefab;
    public Sprite icon;
    public bool isStackable;
    public int maxStack = 1;

    public virtual void Use()   //Нужен не для всех
    {
        //Debug.Log("Предмет использован");
    }
}
