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
    public partial class StatCategoryControl : UserControl, INotifyPropertyChanged
    {
        public StatCategoryControl()
        {
            InitializeComponent();
        }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<int, string> Formatter { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void InitChart()
        {
            Labels = new string[] { Category.계절음식.ToString(), Category.김밥류.ToString(), Category.분식류.ToString(), Category.식사류.ToString() }; 
            OnPropertyChanged("Labels");

            List<Food> lstSeansonFood = App.StatViewModel.item.Where(w => w.Category == Category.계절음식).Cast<Food>().ToList();
            List<Food> lstKimbapFood = App.StatViewModel.item.Where(w => w.Category == Category.김밥류).Cast<Food>().ToList();
            List<Food> lstBunsicFood = App.StatViewModel.item.Where(w => w.Category == Category.분식류).Cast<Food>().ToList();
            List<Food> lstMealnFood = App.StatViewModel.item.Where(w => w.Category == Category.식사류).Cast<Food>().ToList();

            int cntSeasonFood = 0;
            int cntKimbapFood = 0;
            int cntBunsicFood = 0;
            int cntMealFood = 0;

            lstSeansonFood.ForEach(s => cntSeasonFood += s.Orders);
            lstKimbapFood.ForEach(s => cntKimbapFood += s.Orders);
            lstBunsicFood.ForEach(s => cntBunsicFood += s.Orders);
            lstMealnFood.ForEach(s => cntMealFood += s.Orders);

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = new ChartValues<int>() { cntSeasonFood, cntKimbapFood, cntBunsicFood, cntMealFood}
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
}
