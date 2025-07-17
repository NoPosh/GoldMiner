using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework.Constraints;

public class DigComponent : MonoBehaviour
{
    /*
    Копает там где можно (от места)
    Возможно мини игра
    Получение ресурса (от места)

    Нажатие кнопки добыть -> (Мини-игра) -> Получение добычи    
    
    Чем ближе к жиле, тем перспективнее становится добыча
    Проверяет находится ли в жиле, если да, То насколько близко к ней
     */
    public List<BaseGoldMine> goldMines = new List<BaseGoldMine>();
    private InventoryComponent inventoryComponent;

    private void Start()
    {
        inventoryComponent = GetComponent<InventoryComponent>();
    }

    public void Dig()
    {

        if (goldMines.Count > 0)
        {
            int perspective = 0;
            BaseGoldMine currentMine = goldMines[0];
            foreach (BaseGoldMine m in goldMines)
            {
                if (!m.IsActive) continue;
                if ((int)m._MineType > perspective)
                {
                    perspective = (int)m._MineType;
                    currentMine = m; 
                }
            }

            Soil newSoil = currentMine.GetSoil();
            inventoryComponent.TakeSoil(newSoil);

            Debug.Log("Вы выкопали Грунт: " + newSoil.Rare);
        }
        else
        {
            int perspective = 0;
            Soil newSoil = new Soil(perspective);
            inventoryComponent.TakeSoil(newSoil);
            Debug.Log("Вы выкопали Грунт: " + perspective);
        }
    }

    public void AddGoldMine(BaseGoldMine goldMine)
    {
        goldMines.Add(goldMine);
        Debug.Log("Рядом золотая жила: " + Vector3.Distance(transform.position, goldMine.transform.position));
    }

    public void RemoveGoldMine(BaseGoldMine goldMine)
    {
        goldMines.Remove(goldMine);
        Debug.Log("Жила пропала");
    }

}
