using UnityEngine;
using MyGame.EventBus;

public class Recycler : MonoBehaviour, IInteractable
{
    //��� ���������?
    //��� � �����
    //������ �������� � ���������, ����� ���������
    [SerializeField]private RecyclerInventory inputInventory;    
    [SerializeField]private RecyclerInventory outputInventory;

    public RecyclerInventory InputInventory { get { return inputInventory; } }
    public RecyclerInventory OutputInventory { get { return outputInventory; } }
    //������ �����/���� �����������


    public void Interact(GameObject interactor)
    {
        //��������� ���������
        Debug.Log("�������������� � ��������������");
        EventBus.Raise<OnOpenRecycle>(new OnOpenRecycle(this));
    }

    public void StartRecycle()
    {
        //�������� ����������� 
        //������ ���: 
        //1. ���� ������ � ������ ���������
        //2. ������������ �� �����-�� �����
        //3. ��������� ������� "������" � �����-�� ���-�� �� ������ ���������
        //4. ���� ������ ������ ��������������, �� �����������
    }
}
