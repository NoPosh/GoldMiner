using System.Collections.Generic;
namespace MyGame.Core
{
    public class Showcase: Storage
    {
        public List<ShowcaseCell> showcaseCells = new(); //��������� ������ ��� ������������ ���������
        public Showcase(int size): base(size)
        {
            //+��������� ������
            //����� ���-�� ���� ����� (�������, ���-��, ��� ��������, ���� ����)
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

            //+ ����� ����� ����� �������� ��� ������ � ���� (����� ���������� ����� � ��� � ����� ���������� ����� ����)
        }

    }

    public class ShowcaseCell
    {
        public InventoryCell InventoryCell { get; set; }
        public string CellName;
        public int CellCost;
    }
}