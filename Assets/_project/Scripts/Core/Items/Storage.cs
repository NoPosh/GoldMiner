

namespace MyGame.Core
{
    public abstract class Storage
    {
        public Inventory.Inventory Inventory { get; protected set; }
        public bool IsLocked { get; protected set; }

        public Storage(int size)
        {
            Inventory = new MyGame.Inventory.Inventory(size);
        }

        public virtual void Lock() => IsLocked = true;
        public virtual void Unlock() => IsLocked = false;
    }
}