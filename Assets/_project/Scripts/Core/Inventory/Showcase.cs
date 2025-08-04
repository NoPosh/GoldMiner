using System.Collections.Generic;
namespace MyGame.Core
{
    public class Showcase: Storage
    {
        public List<ShowcaseCell> showcaseCells = new(); //Отдельный список для высталвенных предметов
        public Showcase(int size): base(size)
        {
            //+Отдельная логика
            //Нужно что-то типо ячеек (Предмет, кол-во, своё название, своя цена)
        }

        public void SubmitItem(InventoryCell cell, string cellName, int cellCost)
        {
            ShowcaseCell showcaseCell = new ShowcaseCell
            {
                InventoryCell = cell,
                CellName = cellName,
                CellCost = cellCost
            };
            showcaseCells.Add(showcaseCell);

            //+ после этого нужно зарегать эту ячейку в базу (чтобы посетители знали о ней и могли обращаться через базу)
        }

    }

    public class ShowcaseCell
    {
        public InventoryCell InventoryCell { get; set; }
        public string CellName;
        public int CellCost;
    }
}