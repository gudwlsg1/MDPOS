using MDPOS.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDPOS.ViewModel
{
    public partial class FoodViewModel
    {
        public ObservableCollection<Food> StatItems { get; set; }

        internal void Add(ObservableCollection<Food> lstOrder, Pay pay)
        {
            foreach (Food food in lstOrder)
            {
                food.Pay = pay;
                Food Clonefood = (Food)food.Clone();
                Food aFood = StatItems.Where(w => w.Name == Clonefood.Name && w.Pay == pay).FirstOrDefault();

                if (aFood != null)
                {
                    aFood.Count += food.Count;
                    aFood.Total = aFood.Price * aFood.Count;
                    continue;
                }
                
                Clonefood.Total = Clonefood.Price * Clonefood.Count;
                StatItems.Add(Clonefood);
            }
        }

        internal List<Food> GetPayList()
        {
            List<Food> lstPay = new List<Food>();

            for (int i = 0; i < Enum.GetNames(typeof(Pay)).Length; i++)
            {
                lstPay.Add(new Food() { Pay = (Pay)i, Count = 0, Total = 0, Price = 0 });
            }

            foreach (Food food in StatItems)
            {
                Food aFood = lstPay.Where(w => w.Pay == food.Pay).FirstOrDefault();
                aFood.Count += food.Count;
                aFood.Price += food.Price;
                aFood.Total += food.Total;
            }

            return lstPay;
        }

        internal List<Food> GetMenuList()
        {
            List<Food> lstMenu = new List<Food>();

            foreach (Food food in StatItems)
            {
                Food cloneFood = food.Clone() as Food;
                Food aFood = lstMenu.Where(w => w.Name == cloneFood.Name).FirstOrDefault();

                if (aFood != null)
                {
                    aFood.Count += food.Count;
                    aFood.Total = aFood.Price * aFood.Count;
                    continue;
                }
                cloneFood.Total = cloneFood.Price * cloneFood.Count;

                lstMenu.Add(cloneFood);
            }

            return lstMenu;
        }

        internal List<Food> GetCategoryList()
        {
            List<Food> lstCategory = new List<Food>();

            for(int i = 0; i < Enum.GetNames(typeof(Category)).Length; i++)
            {
                lstCategory.Add(new Food() { Category = (Category)i, Count = 0, Total = 0, Price = 0 });
            }

            foreach (Food food in StatItems)
            {
                Food aFood = lstCategory.Where(w => w.Category == food.Category).FirstOrDefault();
                aFood.Count += food.Count;
                aFood.Price += food.Price;
                aFood.Total += food.Price * food.Count;
            }

            return lstCategory;
        }

    }
}