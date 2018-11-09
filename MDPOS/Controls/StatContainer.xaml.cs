using System;
using System.Collections.Generic;
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
    /// Interaction logic for StatControl.xaml
    /// </summary>
    public partial class StatContainer : UserControl
    {
        public StatContainer()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(App.FoodViewModel.StatItems.Count <= 0)
            {
                return;
            }
            ListViewItem listViewItem = lvCondition.SelectedItem as ListViewItem;
            string Condition = (string)listViewItem.Content;

            if (Condition.Equals("메뉴별"))
            {
                SetCtrlVisibility(Visibility.Visible, Visibility.Collapsed, Visibility.Collapsed);

                ctrlStatMenu.InitChart();
            }
            else if(Condition.Equals("카테고리별"))
            {
                SetCtrlVisibility(Visibility.Collapsed, Visibility.Visible, Visibility.Collapsed);

                ctrlStatCategory.InitChart();
            }
            else
            {
                SetCtrlVisibility(Visibility.Collapsed, Visibility.Collapsed, Visibility.Visible);

                ctrlStatPay.InitChart();
            }
        }

        private void SetCtrlVisibility(Visibility MenuVisibility, Visibility CategoryVisibility, Visibility PayVisibility)
        {
            ctrlStatMenu.Visibility = MenuVisibility;
            ctrlStatCategory.Visibility = CategoryVisibility;
            ctrlStatPay.Visibility = PayVisibility;
        }
    }
}
