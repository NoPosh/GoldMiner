using UnityEngine;
using System.Collections.Generic;

public class InventoryComponent : MonoBehaviour
{
    [SerializeField] private int inventoryCapacity = 10;
    public List<Soil> soilInventory = new List<Soil>();

    public void TakeSoil(Soil soil)
    {
        if (soilInventory.Count < inventoryCapacity)
        {
            soilInventory.Add(soil);
            Debug.Log($"Добыт грунт {soil.Rare} уровня");
        }
        else Debug.Log("Недостаточно места");
    }
}

public class Soil
{
    //Редкость (0 - 3 звезды)
    [Range(0, 3)] private int rare;    
    public int Rare { get { return rare; } private set { } }

    public Soil (int rare, float treasurePerspective = 0)
    {
        this.rare = rare;
    }
}
