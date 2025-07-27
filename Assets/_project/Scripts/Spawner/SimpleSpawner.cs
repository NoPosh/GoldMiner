using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MyGame.EventBus;

public class SimpleSpawner : MonoBehaviour
{
    //Висит на спавнере, время от времени передает точку и Ore в спавнер руда

    public SpawnerConfig config;
    private List<GameObject> spawnedObjects = new List<GameObject>();

    private BoxCollider collider;
    private float timer;

    private void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    private void OnEnable()
    {
        EventBus.Subscribe<OnOreCollectedGloabal>(RemoveFromList);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnOreCollectedGloabal>(RemoveFromList);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= config.spawnInterval)
        {
            timer = 0;
            TrySpawn();
        }
    }

    void TrySpawn()
    {
        if (spawnedObjects.Count >= config.maxObjects) return;

        Vector3 spawnPoint = GetRandomPointInZone();
        OreData oreData = PickRandomOre();

        GameObject ore = SpawnerManager.Instance.SpawnOre(oreData, spawnPoint);
        spawnedObjects.Add(ore);
    }

    OreData PickRandomOre() //TODO: сделать возможность по вероятностям
    {
        int index = Random.Range(0, config.possibleOres.Count);
        return config.possibleOres[index];

    }

    Vector3 GetRandomPointInZone()
    {
        BoxCollider box = collider;
        Vector3 localPos = new Vector3(
        Random.Range(-box.size.x / 2, box.size.x / 2),
        Random.Range(-box.size.y / 2, box.size.y / 2),
        Random.Range(-box.size.z / 2, box.size.z / 2)
        );
        Vector3 worldPos = box.transform.TransformPoint(localPos);
        return worldPos;
    }

    void RemoveFromList(OnOreCollectedGloabal e)
    {        
        spawnedObjects.Remove(e.ore.gameObject);
        Debug.Log("Удалили из списка");
    }
}

