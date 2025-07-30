

using System.Numerics;

namespace MyGame.Core.Npc
{
    public class NpcContext
    {
        public NpcData Data { get;}
        public Inventory.Inventory Inventory { get; private set; }

        //� �� ���� ������� ���-�� ���� INpcInteract - ��������� ����, ��� ���������� �������������� � ����������
        //�������� ������� ������ -> �������� ������� ����� ������� ������ �������� (��������� ������� ��� ������� ���������, ����������� � ��)

        public NpcContext(NpcData data) //��� �������������� ������, ����
        {
            Data = data;
        }

        public void UpdateBehavior(float deltaTime) //��� ������ �������� �������
        {

        }

        public void Interact()  //���� �� �������� PlayerContext
        {
            //��� ��� ������� ������ ����������� ������� ��������?
        }

        //������ ��� ����������, �������� �����������
    }
}
