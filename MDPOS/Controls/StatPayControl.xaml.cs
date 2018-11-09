using MDPOS.Model;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for StatPayControl.xaml
    /// </summary>
    public partial class StatPayControl : UserControl
    {
        public StatPayControl()
        {
            InitializeComponent();
        }

        internal void InitChart()
        {
            lvChart.ItemsSource = App.FoodViewModel.GetPayList();
        }
    }
}
