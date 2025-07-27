using UnityEngine;

public class Ore : WorldItem
{
    
    public int Potential {  get; private set; }
    public void Init(OreData data, int potential)
    {
        item = data;
        Potential = potential;
    }
}
