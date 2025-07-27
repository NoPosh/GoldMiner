using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    //Спавнит различные объекты в мире
    public static SpawnerManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject SpawnOre(OreData data, Vector3 position)
    {
        var obj = Instantiate(data.itemPrefab, position, Quaternion.identity);
        int potential = Random.Range(data.potentialRange.x, data.potentialRange.y);
        obj.GetComponent<Ore>().Init(data, potential);
        return obj;
    }
}
