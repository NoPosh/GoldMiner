
using MyGame.Core;

public interface IRecyclingService  //��������� ��� ������ ����� ��������� (� ������� ������ �������� � ��)
{
    public RecyclingResult ProcessItem(BaseItem item, int potential, RecycleMode mode);
}
