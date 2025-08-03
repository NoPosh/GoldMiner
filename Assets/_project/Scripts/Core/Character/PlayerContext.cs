
using MyGame.Inventory;

namespace MyGame.Core
{
    public class PlayerContext
    {
        //Тут можно хранить любые классы, флаги и тд
        //Например инвентарь, инпут, статы персонажа
        public Inventory.Inventory Inventory { get; private set; }
        public Money Money { get; private set; }
        public PlayerContext(Inventory.Inventory inventory)
        {
            Inventory = inventory; 
            Money = new Money();
        }
    

        //Можно передавать этот класс в другие сервисы (крафт, квест, UI) вместо передачи по одному сомпоненту
    }
}
