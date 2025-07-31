
using System.Collections.Generic;
using System.Numerics;

namespace MyGame.Core.Npc
{
    public class NpcContext
    {
        public Inventory.Inventory Inventory { get; private set; }
        public NpcData Data { get;} //��� ���� ������������        

        public List<IInteractionProvider> InteractionProviders = new();   //��, ��� ����� ���������� ��� ��������������
        public List<IInteractionHandler> InteractHandlers = new();        //������ ��������� �������������� - �� �������, ��������� � ��


        public NpcContext(NpcData data)
        {
            Data = data;
        }

        public void UpdateBehavior(float deltaTime) //��� ������ �������� �������
        {

        }

        public void Interact()  //���� �� �������� PlayerContext
        {

        }

    }
}
