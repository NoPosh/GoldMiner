using System.Collections.Generic;

public interface IRecycleRecipeProvider //Интерфейс для данных, которые содержат рецепты
{
    public RecycleRecipe GetRecipe(BaseItem input);
    public IReadOnlyList<RecycleRecipe> Recipes { get; }
}
