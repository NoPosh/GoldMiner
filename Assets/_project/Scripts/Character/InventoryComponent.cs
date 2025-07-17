using UnityEngine;
using System.Collections.Generic;

public class InventoryComponent : MonoBehaviour
{
    /*
    Пока что хранит только грунт    
    */
    public List<Soil> soilInventory = new List<Soil>();

    public void TakeSoil(Soil soil)
    {
        soilInventory.Add(soil);
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
