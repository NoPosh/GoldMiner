using UnityEngine;

public class InteractionContext
{
    public bool IsTrade {  get; set; } //Перенос в рамках торговли
    public bool IsDrop { get; set; } //Игрок выбрасывает
    public bool IsNPC { get; set; } //Взаимодействие с инвентарем NPC
    public bool AllowStealing { get; set; } //Можно ли забрать без покупки
    public bool IsCrafting { get; set; } //Используется в крафте
    public bool IsRecycler { get; set; }
    public object ExtraData { get; set; } //Любые дополнительные данные (например ссылка на квест)

}
