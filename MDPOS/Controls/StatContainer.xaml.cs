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
            ListViewItem listViewItem = lvCondition.SelectedItem as ListViewItem;
            string Condition = (string)listViewItem.Content;

            if (Condition.Equals("메뉴별"))
            {
                ctrlStatCategory.Visibility = Visibility.Collapsed;
                ctrlStatMenu.Visibility = Visibility.Visible;

                ctrlStatMenu.InitChart();
            }
            else
            {
                ctrlStatCategory.Visibility = Visibility.Visible;
                ctrlStatMenu.Visibility = Visibility.Collapsed;

                ctrlStatCategory.InitChart();
            }
        }
    }
}
