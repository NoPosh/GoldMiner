
namespace MyGame.Core
{
    public class Spawner
    {
        private SpawnSettings settings;
        private float timer;

        public int CurrentSpawned {  get; private set; }

        public Spawner(SpawnSettings settings)
        {
            this.settings = settings;
        }

        public bool ShouldSpawn(float deltaTime)
        {
            timer += deltaTime;
            if (timer >= settings.SpawnInterval && CurrentSpawned < settings.MaxObjects)
            {
                timer = 0;
                return true;
            }
            return false;
        }

        public string GetRandomItemId()
        {
            float roll = UnityEngine.Random.value;
            float cumulative = 0f;

            for (int i = 0; i < settings.Entires.Count; i++)
            {
                cumulative += settings.Entires[i].SpawnChance;

                if (roll <= cumulative)
                {
                    return settings.Entires[i].ItemId;
                }

            }
            return settings.Entires[settings.Entires.Count - 1].ItemId;
        }

        public void OnItemSpawned()
        {
            CurrentSpawned++;
        }
        public void OnItemDespawned()
        {
            CurrentSpawned--;
        }
    }
}
