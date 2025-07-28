using UnityEngine;

[CreateAssetMenu(menuName = "Items/BaseItem")]
public class BaseItem : ScriptableObject
{
    public enum ItemType
    {
        None,
        Ore
    }

    //����� ���� ������� � ���������
    public string itemName;
    public string itemDiscription;

    public ItemType itemType;
    public GameObject itemPrefab;
    public Sprite icon;
    public bool isStackable;
    public int maxStack = 1;

    public virtual void Use()   //����� �� ��� ����
    {
        //Debug.Log("������� �����������");
    }
}
