
public class InteractionContext : IInteractionContext
{
    public bool IsTrade {  get; set; } //Перенос в рамках торговли
    public bool IsDrop { get; set; } //Игрок выбрасывает
    public bool IsRecycler { get; set; }

}
