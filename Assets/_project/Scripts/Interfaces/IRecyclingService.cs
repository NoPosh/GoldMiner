
using MyGame.Core;

public interface IRecyclingService  //Интерфейс для разных типов перерабов (с разными базами рецептов и тд)
{
    public RecyclingResult ProcessItem(BaseItem item, int potential, RecycleMode mode);
}
