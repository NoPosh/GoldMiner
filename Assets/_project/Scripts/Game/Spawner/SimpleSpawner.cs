using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MyGame.EventBus;
using MyGame.Core;

public class SimpleSpawner : MonoBehaviour
{
    public SpawnerConfig config;
    [SerializeField] private Transform[] spawnPoints;


    private Spawner spawner;
    private List<GameObject> spawnedObjects = new List<GameObject>();


    private void Awake()
    {
        spawner = new Spawner(ConvertConfig(config));
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
        if (spawner.ShouldSpawn(Time.deltaTime))
        {
            string itemId = spawner.GetRandomItemId();
            SpawnItem(itemId);
        }
    }

    void SpawnItem(string itemId)   //Тут переделать выбор породы
    {
        //Можно искать префаб по ID например через Dictionary или Addressables
        OreData oreData = PickRandomOre();

        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject ore = SpawnerManager.Instance.SpawnOre(oreData, point.position);
        spawnedObjects.Add(ore);
    }

    OreData PickRandomOre() 
    {
        int index = Random.Range(0, config.possibleOres.Count);
        return config.possibleOres[index];

    }


    void RemoveFromList(OnOreCollectedGloabal e)
    {        
        spawnedObjects.Remove(e.ore.gameObject);
    }

    private SpawnSettings ConvertConfig(SpawnerConfig config)
    {
        var settings = new SpawnSettings
        {
            MaxObjects = config.MaxObjects,
            SpawnInterval = config.SpawnInterval
        };

        for (int i = 0; i < this.config.possibleOres.Count; i++)
        {
            settings.Entires.Add(new SpawnEntry
            {
                ItemId = config.possibleOres[i].itemName,
                SpawnChance = config.spawnChances[i],
                PotentialRange = config.possibleOres[i].potentialRange,
                ArtifactChanceRange = config.possibleOres[i].artifactPotentialRange
            });
        }
        return settings;
    }
}

