using UnityEngine;

public class BaseGoldMine : MonoBehaviour
{
    //Можно еще механику, что чем ближе к жиле, тем выше шанс на редкую
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

    public Soil GetSoil()       //Вот тут добавить рандом, который зависит от инструментов, дальности, мини игры и тд
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
                //Делаем неактивной
                isActive = false;
            }
            return new Soil(2);
        }

        return new Soil(0);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;
        //Если зашел персонаж, То добавляем себя ему в список влияющих жил
        if (other.gameObject.name == "Character")
        {
            other.GetComponent<DigComponent>()?.AddGoldMine(this);

        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (!isActive) return; 
        //Вышел - удаляем
        if (other.gameObject.name == "Character")
        {
            other.GetComponent<DigComponent>()?.RemoveGoldMine(this);

        }
    }
}
