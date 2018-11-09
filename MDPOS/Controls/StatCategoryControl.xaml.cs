using LiveCharts;
using LiveCharts.Wpf;
using MDPOS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MDPOS.Controls
{
    /// <summary>
    /// Interaction logic for StatCategoryControl.xaml
    /// </summary>
    public partial class StatCategoryControl : UserControl
    {
        public StatCategoryControl()
        {
            InitializeComponent();
        }
        public void InitChart()
        {
            List<Food> lstStat = App.FoodViewModel.GetCategoryList();
            lvChart.ItemsSource =  lstStat.Where(w => w.Orders >= 1).Cast<Food>().ToList();
        }

    }
}
#if false
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<int, string> Formatter { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void InitChart()
        {
            Labels = new string[] { Category.계절음식.ToString(), Category.김밥류.ToString(), Category.분식류.ToString(), Category.식사류.ToString(), Category.음료.ToString() }; 
            OnPropertyChanged("Labels");

            List<Food> lstSeansonFood = App.FoodViewModel.GetCategoryList(Category.계절음식);
            List<Food> lstKimbapFood = App.FoodViewModel.GetCategoryList(Category.김밥류);
            List<Food> lstBunsicFood = App.FoodViewModel.GetCategoryList(Category.분식류);
            List<Food> lstMealnFood = App.FoodViewModel.GetCategoryList(Category.식사류);
            List<Food> lstDrinkFood = App.FoodViewModel.GetCategoryList(Category.음료);

            int cntSeasonFood = App.FoodViewModel.GetCategoryOrder(Category.계절음식, lstSeansonFood);
            int cntKimbapFood = App.FoodViewModel.GetCategoryOrder(Category.김밥류, lstKimbapFood);
            int cntBunsicFood = App.FoodViewModel.GetCategoryOrder(Category.분식류, lstBunsicFood);
            int cntMealFood = App.FoodViewModel.GetCategoryOrder(Category.식사류, lstMealnFood);
            int cntDrinkFood = App.FoodViewModel.GetCategoryOrder(Category.음료, lstDrinkFood);

            int totalSeasonFood = App.FoodViewModel.GetCategoryTotal(Category.계절음식, lstSeansonFood); 
            int totalKimbapFood = App.FoodViewModel.GetCategoryTotal(Category.김밥류, lstKimbapFood); 
            int totalBunsicFood = App.FoodViewModel.GetCategoryTotal(Category.분식류, lstBunsicFood); 
            int totalMealFood = App.FoodViewModel.GetCategoryTotal(Category.식사류, lstMealnFood);
            int totalDrinkFood = App.FoodViewModel.GetCategoryTotal(Category.음료, lstMealnFood);

            Dictionary<int, int> DictionaryTotal = new Dictionary<int, int>();
            DictionaryTotal.Add(1, totalSeasonFood);
            DictionaryTotal.Add(2, totalKimbapFood);
            DictionaryTotal.Add(3, totalBunsicFood);
            DictionaryTotal.Add(4, totalMealFood);

            SeriesCollection = new SeriesCollection
            { 
                new ColumnSeries
                {
                    Values = new ChartValues<int>() { cntSeasonFood, cntKimbapFood, cntBunsicFood, cntMealFood, cntDrinkFood}
                }
            };

            Formatter = value => value.ToString("N");
            OnPropertyChanged("Formatter");

            DataContext = this;
            OnPropertyChanged("SeriesCollection");
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
#endif