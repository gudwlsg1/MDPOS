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
        public TableInfo TableInfo;
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

        public void AddOrders(Food selectionFood, bool IsMinus = false)
        {
            Food Clonefood = (Food)selectionFood.Clone();

            if (TableInfo.lstOrder.Where(w => w.Name == Clonefood.Name).FirstOrDefault() != null)
            {  // TableInfo.lstOrder에 중복 Food가 존재 할 시
                Food food = TableInfo.lstOrder.Where(w => w.Name == Clonefood.Name).FirstOrDefault();
                if (IsMinus)
                { // -버튼을 클릭할 때
                    food.Orders--;
                    if (food.Orders <= 0)
                    {
                        TableInfo.lstOrder.Remove(food);
                    }
                    return;
                }
                food.Orders++;
                return;
            }
            Clonefood.Orders++;
            TableInfo.lstOrder.Add(Clonefood);
        }

        internal void RemoveOrder(Food selectionFood, bool removeAll = false)
        {
            if (removeAll)
            {
                TableInfo.lstOrder.Clear();
                return;
            }
            TableInfo.lstOrder.Remove(selectionFood);
        }

        internal void Order()
        {
            TableInfo.Orders = string.Empty;
            foreach (Food food in TableInfo.lstOrder)
            {
                TableInfo.Orders += food.Name + " * " + food.Orders + "\n";
            }
        }

        internal void TableInfoClear()
        {
            TableInfo.Orders = string.Empty;
            TableInfo.Total = 0;
            TableInfo.lstOrder.Clear();
            TableInfo.IsOrder = false;
        }
    }
}