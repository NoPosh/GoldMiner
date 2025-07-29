using System.Collections.Generic;

public interface IRecycleRecipeProvider //��������� ��� ������, ������� �������� �������
{
    public RecycleRecipe GetRecipe(BaseItem input);
    public IReadOnlyList<RecycleRecipe> Recipes { get; }
}
