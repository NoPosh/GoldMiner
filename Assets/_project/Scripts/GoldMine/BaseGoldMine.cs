using UnityEngine;

public class BaseGoldMine : MonoBehaviour
{
    //����� ��� ��������, ��� ��� ����� � ����, ��� ���� ���� �� ������
    [SerializeField] protected int usesCount = 1;
    protected bool isActive = true;
    public enum MineType
    {
        Simple,
        Rare,
        Epic
    }

    [SerializeField] protected MineType _mineType = MineType.Simple;
    public MineType _MineType { get { return _mineType; } private set { } }

    public Soil GetSoil()       //��� ��� �������� ������, ������� ������� �� ������������, ���������, ���� ���� � ��
    {
        if (!isActive) return new Soil(0);

        if (_mineType == MineType.Simple)
        {
            return new Soil(1);
        }
        else if (_mineType == MineType.Rare)
        {
            usesCount--;
            if (usesCount == 0)
            {
                //������ ����������
                isActive = false;
            }
            return new Soil(2);
        }

        return new Soil(0);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;
        //���� ����� ��������, �� ��������� ���� ��� � ������ �������� ���
        if (other.gameObject.name == "Character")
        {
            other.GetComponent<DigComponent>()?.AddGoldMine(this);

        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (!isActive) return; 
        //����� - �������
        if (other.gameObject.name == "Character")
        {
            other.GetComponent<DigComponent>()?.RemoveGoldMine(this);

        }
    }
}
