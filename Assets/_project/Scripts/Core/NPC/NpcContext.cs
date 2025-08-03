
using System;
using System.Collections.Generic;
using System.Numerics;

namespace MyGame.Core.Npc
{
    public class NpcContext
    {
        public Inventory.Inventory Inventory { get; private set; }  //Не факт что нужен всем
        public Money Money { get; private set; }

        public NpcData Data { get;}
        private bool CanInteract = true;

        public event Action OnDialogueStart;

        public NpcContext(NpcData data, int inventorySize, int money = 0)
        {
            Data = data;
            Money = new Money(100);
            Inventory = new Inventory.Inventory(inventorySize);
        }

        public void UpdateBehavior(float deltaTime) //Тут логика принятия решений
        {

        }

        public void Interact()  //Сюда бы добавить PlayerContext
        {
            //Пока что просто диалог
            if (CanInteract && Data.dialog != null)
            {
                StartDialogue();
            }

        }

        private void StartDialogue()
        {
            OnDialogueStart?.Invoke();
        }

    }
}
