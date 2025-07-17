using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework.Constraints;

public class DigComponent : MonoBehaviour
{
    /*
    ������ ��� ��� ����� (�� �����)
    �������� ���� ����
    ��������� ������� (�� �����)

    ������� ������ ������ -> (����-����) -> ��������� ������    
    
    ��� ����� � ����, ��� ������������� ���������� ������
    ��������� ��������� �� � ����, ���� ��, �� ��������� ������ � ���
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

            Debug.Log("�� �������� �����: " + newSoil.Rare);
        }
        else
        {
            int perspective = 0;
            Soil newSoil = new Soil(perspective);
            inventoryComponent.TakeSoil(newSoil);
            Debug.Log("�� �������� �����: " + perspective);
        }
    }

    public void AddGoldMine(BaseGoldMine goldMine)
    {
        goldMines.Add(goldMine);
        Debug.Log("����� ������� ����: " + Vector3.Distance(transform.position, goldMine.transform.position));
    }

    public void RemoveGoldMine(BaseGoldMine goldMine)
    {
        goldMines.Remove(goldMine);
        Debug.Log("���� �������");
    }

}
