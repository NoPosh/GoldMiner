using UnityEngine;
using System.Collections.Generic;
using MyGame.Core;

[CreateAssetMenu(fileName = "RecycleRecipeDatabase", menuName = "Game/RecycleRecipeDatabase")]
public class RecycleRecipeDatabase : ScriptableObject, IRecycleRecipeProvider
{
    [SerializeField] private List<RecycleRecipe> recipes = new();

    public RecycleRecipe GetRecipe(BaseItem input)
    {
        return recipes.Find(r => r.InputItem == input);
    }

    public IReadOnlyList<RecycleRecipe> Recipes => recipes;
}

[System.Serializable]
public class RecycleRecipe
{
    public BaseItem InputItem;
    public BaseItem OutputItem;
    public int BaseAmount;  //Сколько выдается без множителей
    public float time;  //Длительность переработки
}
