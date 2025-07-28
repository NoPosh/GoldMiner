using UnityEngine;

namespace MyGame.Core
{
    public abstract class Storage
    {
        public Inventory.Inventory Inventory { get; private set; }
        public bool IsLocked { get; private set; }

        public Storage(int size)
        {
            Inventory = new MyGame.Inventory.Inventory(size);
        }

        public virtual void Lock() => IsLocked = true;
        public virtual void Unlock() => IsLocked = false;
    }
}