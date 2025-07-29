
using System;
using System.Collections.Generic;

namespace MyGame.Core
{
    public class RecyclingService : IRecyclingService
    {
        private IRecycleRecipeProvider _recycleRecipeProvider;

        public RecyclingService(IRecycleRecipeProvider recycleRecipeProvider)
        {
            _recycleRecipeProvider = recycleRecipeProvider;
        }

        public RecyclingResult ProcessItem(BaseItem item, int potential, RecycleMode mode)
        {
            var recipe = _recycleRecipeProvider.GetRecipe(item);

            if (recipe == null) return RecyclingResult.Empty;

            //��������� � ����������� �� ��������
            int multiplier = 1;

            //������� ���������� (�������� ������� ��� �� ������)
            multiplier *= potential;

            switch (mode)
            {
                case RecycleMode.Normal: multiplier += 1; break;
                case RecycleMode.Efficient: multiplier += 3; break;
            }

            //��������� � ��

            //��� ����� ������� ���-�� ���� �������, ������� �������� �������� �� �������� => 
            var result = new RecyclingResult(recipe.OutputItem, recipe.BaseAmount * multiplier);
            return result;
        }
    }

    public class RecyclingResult
    {
        public BaseItem Item { get; }
        public int Amount { get; }

        public static RecyclingResult Empty => new RecyclingResult(null, 0);

        public RecyclingResult (BaseItem item, int amount)
        {
            Item = item;
            Amount = amount;
        }
    }

    public enum RecycleMode
    {
        Normal,
        Fast,
        Efficient
    }


}