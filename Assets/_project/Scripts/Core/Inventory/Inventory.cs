using System.Collections.Generic;
using MyGame.EventBus;
using System.Drawing;
using System;
using UnityEditorInternal.Profiling.Memory.Experimental;

namespace MyGame.Inventory
{
    public class Inventory
    {
        private readonly List<InventoryCell> _cells = new();
        public IReadOnlyList<InventoryCell> Cells => _cells;

        public int Size { get; }

        public event Action OnInventoryChanged;

        public Inventory(int size)
        {
            Size = size;
            Init();
        }

        private void Init()
        {
            if (_cells.Count == 0 )
                for (int i = 0; i < Size; i++)
                {
                    InventoryCell cell = new InventoryCell(i, this);
                    _cells.Add(cell);
                }
        }

        public int AddItem(BaseItem newItem, int amount = 1)    //Возвращает сколько не смогли добавить
        {
            if (newItem.isStackable)
            {
                foreach (InventoryCell cell in _cells)
                {
                    if (cell.item == newItem && cell.amount < newItem.maxStack)
                    {
                        int space = newItem.maxStack - cell.amount;
                        int toAdd = Math.Min(space, amount);
                        cell.amount += toAdd;
                        amount -= toAdd;

                        OnInventoryChanged?.Invoke();
                        if (amount <= 0)                           
                            return 0;
                    }
                }
            }

            foreach (InventoryCell cell in _cells)
            {
                if (cell.IsEmpty)
                {
                    int toAdd = Math.Min(newItem.maxStack, amount);
                    cell.item = newItem;
                    cell.amount = toAdd;
                    amount -= toAdd;

                    OnInventoryChanged?.Invoke();
                    if (amount <= 0) return 0;
                }
            }
            OnInventoryChanged?.Invoke();
            return amount;
        }

        public virtual void RemoveItem(int index, int amount = 1)
        {
            _cells[index].Clear();
            OnInventoryChanged?.Invoke();
        }

        public virtual bool MoveItem(int fromIndex, int toIndex)
        {
            if (toIndex == -1) return false;
            if (fromIndex == toIndex) return false;

            var fromCell = _cells[fromIndex];
            var toCell = _cells[toIndex];

            if (!fromCell.IsEmpty && !toCell.IsEmpty)
            {
                if (fromCell.item.isStackable && fromCell.item == toCell.item)
                {
                    int space = toCell.item.maxStack - toCell.amount;
                    int toAdd = Math.Min(space, fromCell.amount);
                    toCell.amount += toAdd;
                    fromCell.amount -= toAdd;
                    if (fromCell.amount <= 0) fromCell.Clear();
                    OnInventoryChanged?.Invoke();
                    return true;
                }
                (toCell.item, fromCell.item) = (fromCell.item, toCell.item);
                (toCell.amount, fromCell.amount) = (fromCell.amount, toCell.amount);
            }
            else
            {
                toCell.item = fromCell.item;
                toCell.amount = fromCell.amount;

                fromCell.Clear();
            }
            OnInventoryChanged?.Invoke();
            return true;
        }

        public static bool MoveItemBetween(Inventory fromInv, int fromIndex, Inventory toInv, int toIndex)
        {
            if (fromIndex == -1 || toIndex == -1) return false;
           

            var fromCell = fromInv._cells[fromIndex];
            var toCell = toInv._cells[toIndex];

            // Обмен местами (если оба слота заняты)
            if (!fromCell.IsEmpty && !toCell.IsEmpty)
            {
                if (fromCell.item.isStackable && fromCell.item == toCell.item)
                {
                    int space = toCell.item.maxStack - toCell.amount;
                    int toAdd = Math.Min(space, fromCell.amount);
                    toCell.amount += toAdd;
                    fromCell.amount -= toAdd;
                    if (fromCell.amount <= 0) fromCell.Clear();

                    fromInv.OnInventoryChanged?.Invoke();
                    toInv.OnInventoryChanged?.Invoke();
                    return true;
                }
                (toCell.item, fromCell.item) = (fromCell.item, toCell.item);
                (toCell.amount, fromCell.amount) = (fromCell.amount, toCell.amount);
            }
            else // Просто перенос
            {
                toCell.item = fromCell.item;
                toCell.amount = fromCell.amount;

                fromCell.Clear();
            }
            fromInv.OnInventoryChanged?.Invoke();
            toInv.OnInventoryChanged?.Invoke();
            return true;
        }

        public InventoryCell GetCell(int index)
        {
            return _cells[index];
        }

        public bool IsEmpty()
        {
            foreach (var cell in _cells)
            {
                if (!cell.IsEmpty) return false;
            }
            return true;
        }

        public InventoryCell GetFirstNonEmptyCell()
        {
            foreach (var cell in _cells)
            {
                if (!cell.IsEmpty) return cell;
            }
            return null;
        }

        public InventoryCell GetFirstEmptyCell()
        {
            foreach (var cell in _cells)
            {
                if (cell.IsEmpty) return cell;
            }
            return null;
        }
    }

}