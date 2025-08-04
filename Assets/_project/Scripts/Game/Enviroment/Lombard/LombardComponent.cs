using MyGame.Core;
using UnityEngine;

public class LombardComponent : MonoBehaviour
{
    private LombardNpcGenerator _generator;
    [SerializeField] private Transform _spawnPoint;

    private void Awake()
    {
        _generator = new LombardNpcGenerator();
        _generator.OnCustomerSpawn += SpawnNpc;
        _generator.ToggleLombard();
    }

    private void OnDisable()
    {
        _generator.OnCustomerSpawn -= SpawnNpc;
    }

    private void Update()
    {
        _generator.Update(Time.deltaTime);
    }

    private void SpawnNpc()
    {
        Debug.Log("Заспавнили НПС");
        //Пока что в определенной точке создаем
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        obj.layer = LayerMask.NameToLayer("Item");
        obj.transform.position = _spawnPoint.position;

        NpcComponent npcComponent = obj.AddComponent<NpcComponent>();
        npcComponent.Init(_generator._currentNpc, true);
    }
}
