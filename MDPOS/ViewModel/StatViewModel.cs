using MDPOS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDPOS.ViewModel
{
    public class StatViewModel
    {
        public ObservableCollection<Food> item;

        public StatViewModel()
        {
            item = new ObservableCollection<Food>();
        }

        internal void Add(ObservableCollection<Food> lstOrder)
        {
            foreach (Food food in lstOrder)
            {
                Food Clonefood = (Food)food.Clone();
                Food aFood = App.StatViewModel.item.Where(w => w.Name == Clonefood.Name).FirstOrDefault();

                if (aFood != null)
                {
                    aFood.Orders += food.Orders;
                    continue;
                }

                App.StatViewModel.item.Add(Clonefood);
            }
        }

        internal List<Food> GetCategoryList(Category category)
        {
            return App.StatViewModel.item.Where(w => w.Category == category).Cast<Food>().ToList();
        }

        internal int GetCategoryOrder(Category category, List<Food> lstOrder)
        {
            int sum = 0;
            lstOrder.ForEach(s => sum += s.Orders);
            return sum;
        }

        internal int GetCategoryTotal(Category category, List<Food> lstOrder)
        {
            int total = 0;
            lstOrder.ForEach(s => total += s.Price * s.Orders);
            return total;
        }
    }
}
