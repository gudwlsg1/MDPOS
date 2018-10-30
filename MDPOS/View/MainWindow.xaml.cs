using MDPOS.Controls;
using MDPOS.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MDPOS
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        string Time;
        DateTime dateTime;
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= MainWindow_Loaded;

            InitData();
        }

        private void InitData()
        {
            lvTable.ItemsSource = App.TableViewModel.Items;
            lvFunction.SelectedIndex = 0;
            InitTimer();
        }

        private void InitTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(1000);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            dateTime = DateTime.Now;

            if (dateTime.Hour == 0)
            {
                App.StatViewModel.item.Clear();
            }

            Time = string.Format("{0:g}", dateTime);
            tbTime.Text = Time;
        }

        private void lvFunction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewItem selectedFunction = lvFunction.SelectedItem as ListViewItem;
            string function = selectedFunction.Content as string;

            if (function.Equals("좌석"))
            {
                ctrlStatContainer.Visibility = Visibility.Collapsed;
            }
            else // 통계일때
            { // 통계 -> 메뉴 or 카테고리별 클릭 -> Chart 초기화
                ctrlStatContainer.Visibility = Visibility.Visible;
                ctrlStatContainer.lvCondition.SelectedIndex = 0;
            }
        }

        private void lvTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TableInfo selectionTable = lvTable.SelectedItem as TableInfo;
            selectionTable.OrderTime = DateTime.Now;

            ctrlFood.Visibility = Visibility.Visible;
            ctrlFood.selectedTable(selectionTable.Number);
        }
    }
}
