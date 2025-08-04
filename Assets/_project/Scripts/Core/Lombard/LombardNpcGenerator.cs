using MyGame.Core.Npc;
using System;

namespace MyGame.Core
{
    public class LombardNpcGenerator
    {
        public NpcContext _currentNpc {  get; private set; }
        private float spawnInterval = 5;
        private float timer = 0;
        public bool IsOpen { get; private set; }

        public event Action OnCustomerSpawn;
        public void Update(float deltaTime)
        {
            if (!IsOpen) return;

            if (_currentNpc != null) return;

            if (timer >= spawnInterval)
            {
                _currentNpc = SpawnNpc();
                OnCustomerSpawn?.Invoke();

                timer = 0;
                return;
            }
            timer += deltaTime;
        }

        private NpcContext SpawnNpc()
        {
            //Вообще тут надо бы через сервис как-то получать такого случайного NPC
            return Services.CreatingNpcService.GetRandomNpc();
        }

        public void ToggleLombard()
        {
            IsOpen = !IsOpen;
        }
    }
}