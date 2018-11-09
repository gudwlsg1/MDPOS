using MDPOS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDPOS.ViewModel
{
    public class TableViewModel
    {
        public ObservableCollection<TableInfo> Items;
        public List<Food> lstBeforeFood;

        public TableViewModel()
        {
            Items = new ObservableCollection<TableInfo>();
            InitDate();
            lstBeforeFood = new List<Food>();
        }

        private void InitDate()
        {
            for (int i = 1; i < 11; i++)
            {
                Items.Add(new TableInfo() { Number = i, OrderTime = DateTime.Now, lstOrder = new ObservableCollection<Food>() });
            }
        }

        public void AddOrders(Food selectionFood, int tableNum, bool IsMinus = false)
        {
            Food Clonefood = (Food)selectionFood.Clone();
            TableInfo tableInfo = GetTable(tableNum);

            if (tableInfo.lstOrder.Where(w => w.Name == Clonefood.Name).FirstOrDefault() != null)
            {  // TableInfo.lstOrder에 중복 Food가 존재 할 시
                Food food = tableInfo.lstOrder.Where(w => w.Name == Clonefood.Name).FirstOrDefault();
                if (IsMinus)
                { // -버튼을 클릭할 때
                    food.Orders--;
                    if (food.Orders <= 0)
                    {
                        tableInfo.lstOrder.Remove(food);
                    }
                    return;
                }
                food.Orders++;
                return;
            }
            Clonefood.Orders++;
            tableInfo.lstOrder.Add(Clonefood);
        }
        internal void RemoveOrder(Food selectionFood,int talbeNum, bool removeAll = false)
        {
            TableInfo tableInfo = GetTable(talbeNum);
            if (removeAll)
            {
                tableInfo.lstOrder.Clear();
                return;
            }
            tableInfo.lstOrder.Remove(selectionFood);
        }

        internal void Order(int tableNum)
        {
            TableInfo tableInfo = GetTable(tableNum);

            tableInfo.Orders = string.Empty;
            foreach (Food food in tableInfo.lstOrder)
            {
                tableInfo.Orders += food.Name + " * " + food.Orders + "\n";
            }
            tableInfo.OrderTime = DateTime.Now;
        }

        internal void TableInfoClear(int tableNum)
        {
            TableInfo tableInfo = GetTable(tableNum);

            tableInfo.Orders = string.Empty;
            tableInfo.Total = 0;
            tableInfo.lstOrder.Clear();
            tableInfo.IsOrder = false;
        }

        internal TableInfo GetTable(int tableNum)
        {
            return Items.Where(w => w.Number == tableNum).FirstOrDefault();
        }
    }
}