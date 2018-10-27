using MDPOS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for FoodControl.xaml
    /// </summary>

    public enum ActionOrder
    {
        btnBack,
        btnOrder,
        btnCancle,
        btnAllCancle,
        btnOrderAdd,
        btnOrderSub
    }
    public partial class FoodControl : UserControl
    {

       // TableInfo TableInfo = new TableInfo();

        public FoodControl()
        {
            InitializeComponent();
            this.Loaded += FoodControl_Loaded;
        }

        private void FoodControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= FoodControl_Loaded;

            InitDate();
        }

        private void InitDate()
        {
            lvFood.ItemsSource = App.FoodViewModel.Items;

          //  this.DataContext = App.TableViewModel.TableInfo;
        }

        #region 카테고리 변경
        private void lvCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string Category = (lvCategory.SelectedItem as ListBoxItem).Content.ToString();
            ChangelvFood(Category);
        }
        private void ChangelvFood(string Category)
        {
            List<Food> lstFood = new List<Food>();
            if (Category.Equals("전체"))
            {
                lvFood.ItemsSource = App.FoodViewModel.Items;
                return;
            }
            else
            {
                foreach (Food food in App.FoodViewModel.Items)
                {
                    if (Category.Equals(food.Category.ToString()))
                    {
                        lstFood.Add(food);
                    }
                }
                lvFood.ItemsSource = lstFood;
            }
        }
        #endregion

        private void lvFood_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvFood.SelectedItem == null)
            {
                return;
            }
            Food food = lvFood.SelectedItem as Food;
            App.TableViewModel.AddOrders(food);
            InitOrder();
        }

        private void lvFood_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            lvFood_SelectionChanged(sender, null);
        }

        private void lvOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EnableBtn(true);

            Food food = lvOrders.SelectedItem as Food;
            InitImg(food);
            return;
        }

        private void EnableBtn(bool action)
        {
            btnCancle.IsEnabled = action;
            btnOrderAdd.IsEnabled = action;
            btnOrderSub.IsEnabled = action;
        }

        public void selectedTable(object sender, TableInfo selectionTable)
        {
            App.TableViewModel.TableInfo = selectionTable;
            // App.TableViewModel.TableInfo.Number = selectionTable.Number;
            // App.TableViewModel.TableInfo.OrderTime = DateTime.Now;
            // App.TableViewModel.TableInfo = App.TableViewModel.TableInfo;

            this.DataContext = App.TableViewModel.TableInfo;
            lvFood.SelectedItem = null;


            CheckIsOrder();
            InitOrder();

            App.TableViewModel.lstBeforeFood.Clear();
            App.TableViewModel.TableInfo.lstOrder.ToList().ForEach(s => App.TableViewModel.lstBeforeFood.Add((Food)s.Clone()));
        }

        private void CheckIsOrder()
        {
            if (App.TableViewModel.TableInfo.lstOrder.Count > 0)
            {
                App.TableViewModel.TableInfo.IsOrder = true;
            }
            else
            {
                App.TableViewModel.TableInfo.IsOrder = false;
            }
        }

        private void InitOrder()
        {
            lvOrders.ItemsSource = App.TableViewModel.TableInfo.lstOrder;

            int total = 0;
            foreach(Food food in App.TableViewModel.TableInfo.lstOrder)
            {
                total += food.Orders * food.Price;
            }
            tbPrice.Text = total.ToString();
            return;
        }
        #region Food Back, 주문, +, -, 취소, 전체취소
        private void ActionOrderClick(object sender, RoutedEventArgs e)
        {
            //TODO: 코드를 더욱 깔끔히!, Back - 주문, 각각 하나씩 btnOrder을 정해준다.
            Button button = sender as Button;
            string btnName = button.Name;
            Food food = lvOrders.SelectedItem as Food;

            if (btnName.Equals(ActionOrder.btnBack.ToString()))
            {// Back
                this.Visibility = Visibility.Collapsed;
                if (App.TableViewModel.TableInfo.IsOrder)
                {
                    if (!App.TableViewModel.lstBeforeFood.Equals(App.TableViewModel.TableInfo.lstOrder))
                    { // 이전 주문목록이랑 다를 때
                        App.TableViewModel.TableInfo.lstOrder = new ObservableCollection<Food>(App.TableViewModel.lstBeforeFood);
                        return;
                    }
                    return;
                }
                App.TableViewModel.RemoveOrder(food, true);
            }
            else if (btnName.Equals(ActionOrder.btnOrder.ToString()))
            {// 주문
                this.Visibility = Visibility.Collapsed;
                EnableBtn(false);
                App.TableViewModel.TableInfo.IsOrder = true;
                if (App.TableViewModel.TableInfo.lstOrder.Count == 0)
                {
                    return;
                }
                App.TableViewModel.Order();
            }
        }
        /// <summary>
        /// Food +, - 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActionAddFood(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string btnName = button.Name;
            Food food = lvOrders.SelectedItem as Food;

            if (btnName.Equals(ActionOrder.btnOrderAdd.ToString()))
            {// +
                App.TableViewModel.AddOrders(food);
            }
            else if (btnName.Equals(ActionOrder.btnOrderSub.ToString()))
            {// -
                App.TableViewModel.AddOrders(food, true);
            }

            InitOrder();
        }

        /// <summary>
        /// Food 취소, 전체취소
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActionCancleFood(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string btnName = button.Name;
            Food food = lvOrders.SelectedItem as Food;

            if (btnName.Equals(ActionOrder.btnCancle.ToString()))
            {// 취소
                App.TableViewModel.RemoveOrder(food);
            }
            else if (btnName.Equals(ActionOrder.btnAllCancle.ToString()))
            {// 전체취소
                App.TableViewModel.RemoveOrder(null, true);
                lvOrders.SelectedItem = null;
            }
            InitOrder();
        }
        #endregion
        private void InitImg(Food food)
        {
            if(food == null)
            {
                lvOrders.SelectedItem = null;
                imgFood.Source = new BitmapImage(new Uri("/Resources/camera.png", UriKind.Relative));
                EnableBtn(false);
                return;
            }
            imgFood.Source = new BitmapImage(new Uri(food.Picture, UriKind.Relative));
        }
        
        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            if (App.TableViewModel.TableInfo.lstOrder.Count < 0)
            {
                return;
            }

            string Pay = string.Empty;

            if ((bool)btnCard.IsChecked)
            {
                Pay = (string)btnCard.Content;
            }
            else
            {
                Pay = (string)btnMoney.Content;
            }

            string Title = "결제확인";
            App.TableViewModel.Order();
            MessageBoxResult messageBoxResult = MessageBox.Show(Pay + "\n" + App.TableViewModel.TableInfo.Orders, Title, MessageBoxButton.YesNo);

            if((int)messageBoxResult == (int)MessageBoxResult.Yes)
            {
                App.StatViewModel.Add(App.TableViewModel.TableInfo.lstOrder);
                this.Visibility = Visibility.Collapsed;
                App.TableViewModel.TableInfoClear();
            }
            return;
        }
    }
}