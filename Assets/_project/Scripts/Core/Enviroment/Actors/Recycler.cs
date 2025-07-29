
using System;

namespace MyGame.Core
{
    public class Recycler
    {
        public Inventory.Inventory InputInventory {  get; private set; }
        public Inventory.Inventory OutputInventory { get; private set; }
        public bool IsProcessing { get; private set; }
        public float ProcessTime { get; private set; } = 10f; //Потом это нужно будет исправить, тк разное перерабатывается по-разному?

        public event Action OnStartRecycle;
        public event Action OnStopRecycle;

        private IRecyclingService recyclingService;
        private IRecycleRecipeProvider recipeProvider;
        private float timer;

        //Можно добавить Event запуска/остановки перераба, чтобы обертка улавливала

        public Recycler(int inputSize, int outputSize, IRecyclingService recycleService, IRecycleRecipeProvider recipeProvider)
        {
            InputInventory = new Inventory.Inventory(inputSize);
            OutputInventory = new Inventory.Inventory(outputSize);
            this.recyclingService = recycleService;
            this.recipeProvider = recipeProvider;
        }

        public void StartProcessing()
        {
            if (!IsProcessing && !InputInventory.IsEmpty())
            {
                var recipe = recipeProvider.GetRecipe(InputInventory.GetFirstNonEmptyCell().item);
                if (recipe == null)
                {
                    return;
                }
                IsProcessing = true;
                timer = 0f;
                ProcessTime = recipeProvider.GetRecipe(InputInventory.GetFirstNonEmptyCell().item).time;
                OnStartRecycle?.Invoke();
            }
        }

        public void StopProcessing()
        {
            OnStopRecycle?.Invoke();
            IsProcessing = false;
        }

        public void Update(float deltaTime)
        {
            if (!IsProcessing) return;
            if (InputInventory.IsEmpty())
            {
                StopProcessing();
                return;
            }

            if (OutputInventory.GetFirstEmptyCell() == null)
            {
                StopProcessing();
                return;
            }

            timer += deltaTime;
            if (timer > ProcessTime)
            {
                timer = 0f;
                ProcessNextItem();
            }
        }

        private void ProcessNextItem()
        {
            var cell = InputInventory.GetFirstNonEmptyCell();
            if (cell == null) return;

            var resut = recyclingService.ProcessItem(cell.item, cell.item.GetPotential(), RecycleMode.Normal);
            //+ можно сделать еще один сервис, который выбирает выдавать ли артефакты и другие вещи

            if (OutputInventory.AddItem(resut.Item, resut.Amount))
                InputInventory.RemoveItem(cell.index, 1);
            
                
        }
    }
}
