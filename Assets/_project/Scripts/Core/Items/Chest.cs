using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.Core
{
    public class Chest : Storage
    {
        public string ChestId { get; private set; } //Чтобы запоминать содержимое в SaveSystem
        public bool RespawnLoot { get; private set; }

        public Chest(string chestId, int size, bool respawnLoot = false) : base(size)
        {
            ChestId = chestId;
            RespawnLoot = respawnLoot;
        }

        public void SpawnLoot(IEnumerable<BaseItem> loot)
        {
            foreach (var item in loot)
            {
                Inventory.AddItem(item);
            }
        }
    }
}
