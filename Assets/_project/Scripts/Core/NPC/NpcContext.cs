

using System.Numerics;

namespace MyGame.Core.Npc
{
    public class NpcContext
    {
        public NpcData Data { get;}
        public Inventory.Inventory Inventory { get; private set; }

        //Я бы сюда добавил что-то типо INpcInteract - интерфейс того, как происходит взаимодействие с персонажем
        //Например открыть диалог -> варианты диалога могут вызвать разные действия (следующая реплика или открыть инвентарь, торговаться и тд)

        public NpcContext(NpcData data) //Тут инициализируем список, Дату
        {
            Data = data;
        }

        public void UpdateBehavior(float deltaTime) //Тут логика принятия решений
        {

        }

        public void Interact()  //Сюда бы добавить PlayerContext
        {
            //Тут для каждого списка компонентов сделать интеракт?
        }

        //Методы для добавления, удаления компонентов
    }
}
