
using System.Collections.Generic;
using System.Numerics;

namespace MyGame.Core.Npc
{
    public class NpcContext
    {
        public Inventory.Inventory Inventory { get; private set; }
        public NpcData Data { get;} //Тут лист способностей        

        public List<IInteractionProvider> InteractionProviders = new();   //То, что может предложить при взаимодействии
        public List<IInteractionHandler> InteractHandlers = new();        //Сервис обработки взаимодействия - от времени, контекста и тд


        public NpcContext(NpcData data)
        {
            Data = data;
        }

        public void UpdateBehavior(float deltaTime) //Тут логика принятия решений
        {

        }

        public void Interact()  //Сюда бы добавить PlayerContext
        {

        }

    }
}
