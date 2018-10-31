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
using LiveCharts;
using LiveCharts.Wpf;
using MDPOS.Model;

namespace MDPOS.Controls
{
    /// <summary>
    /// Interaction logic for MenuStatControl.xaml
    /// </summary>
    public partial class StatMenuControl : UserControl, INotifyPropertyChanged
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<int, string> Formatter { get; set; }

        public StatMenuControl()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void InitChart()
        {
            Labels = App.StatViewModel.item.Select(s => s.Name).ToArray();
            OnPropertyChanged("Labels");

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = new ChartValues<int>(App.StatViewModel.item.Select(w => w.Orders)),
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
