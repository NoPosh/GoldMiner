
using MyGame.Inventory;

namespace MyGame.Core
{
    public class PlayerContext
    {
        //��� ����� ������� ����� ������, ����� � ��
        //�������� ���������, �����, ����� ���������
        public Inventory.Inventory Inventory { get; private set; }

        public PlayerContext(Inventory.Inventory inventory)
        {
            Inventory = inventory; 
        }
    

        //����� ���������� ���� ����� � ������ ������� (�����, �����, UI) ������ �������� �� ������ ����������
    }
}
