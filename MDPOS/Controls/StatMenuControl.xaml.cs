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
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using MDPOS.Model;

namespace MDPOS.Controls
{
    /// <summary>
    /// Interaction logic for MenuStatControl.xaml
    /// </summary>
    public partial class StatMenuControl : UserControl
    {
        public StatMenuControl()
        {
            InitializeComponent();
        }

        public void InitChart()
        {
            lvChart.ItemsSource = App.FoodViewModel.GetMenuList();
        }

    }
}