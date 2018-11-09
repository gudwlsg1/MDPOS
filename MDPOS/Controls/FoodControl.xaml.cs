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
        private string BarCode = string.Empty;
        private TableInfo TableInfo;

        public FoodControl()
        {
            InitializeComponent();
            this.Loaded += FoodControl_Loaded;
        }

        private void FoodControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= FoodControl_Loaded;
            this.PreviewTextInput += FoodControl_PreviewTextInput;
            
            InitData();
        }

        private void FoodControl_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.Equals("\r"))
            {
                AddBarcodeFood();
                BarCode = string.Empty;
                return;
            }
            BarCode += e.Text;
        }

        internal void AddBarcodeFood()
        {
            Food food = App.FoodViewModel.GetFood(BarCode);
            if(food != null && TableInfo != null)
            {
                App.TableViewModel.AddOrders(food, TableInfo.Number);
            }

            InitOrder();
        }

        private void InitData()
        {
            lvFood.ItemsSource = App.FoodViewModel.Items;
        }

        #region 카테고리 변경
        private void lvCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string Category = (lvCategory.SelectedItem as ListBoxItem).Content.ToString();
            ChangelvFood(Category);
        }
        private void ChangelvFood(string Category)
        {
            if (Category.Equals("전체"))
            {
                lvFood.ItemsSource = App.FoodViewModel.Items;
            }
            else
            {
                lvFood.ItemsSource = App.FoodViewModel.GetCategoryFood(Category);
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
            App.TableViewModel.AddOrders(food, TableInfo.Number);
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

        public void selectedTable(int tableNum)
        {
            TableInfo = App.TableViewModel.GetTable(tableNum);
            if(TableInfo == null)
            {
                MessageBox.Show("테이블에 관한 예외처리가 존재합니다.");
                this.Visibility = Visibility.Collapsed;
                return;
            }

            tbTableNum.Text = TableInfo.Number.ToString();
            tbOrderTime.Text = TableInfo.OrderTime.ToString();

            lvFood.SelectedItem = null;

            CheckIsOrder();
            InitOrder();

            App.TableViewModel.lstBeforeFood.Clear();
            TableInfo.lstOrder.ToList().ForEach(s => App.TableViewModel.lstBeforeFood.Add((Food)s.Clone()));
        }

        private void CheckIsOrder()
        {
            if (TableInfo.lstOrder.Count > 0)
            {
                TableInfo.IsOrder = true;
                btnPay.IsEnabled = true;
            }
            else
            {
                TableInfo.IsOrder = false;
                btnPay.IsEnabled = false;
            }
        }

        private void InitOrder()
        {
            lvOrders.ItemsSource = TableInfo.lstOrder;

            int total = 0;
            foreach(Food food in TableInfo.lstOrder)
            {
                total += food.Orders * food.Price;
            }
            tbPrice.Text = total.ToString();
            return;
        }
        #region Food Back, 주문, +, -, 취소, 전체취소
        private void ActionOrderClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string btnName = button.Name;
            Food food = lvOrders.SelectedItem as Food;

            if (btnName.Equals(ActionOrder.btnBack.ToString()))
            {// Back
                this.Visibility = Visibility.Collapsed;
                if (TableInfo.IsOrder)
                {
                    if (!App.TableViewModel.lstBeforeFood.Equals(TableInfo.lstOrder))
                    { // 이전 주문목록이랑 다를 때
                        TableInfo.lstOrder = new ObservableCollection<Food>(App.TableViewModel.lstBeforeFood);
                        return;
                    }
                    return;
                }
                App.TableViewModel.RemoveOrder(food, TableInfo.Number, true);
            }
            else if (btnName.Equals(ActionOrder.btnOrder.ToString()))
            {// 주문
                this.Visibility = Visibility.Collapsed;
                EnableBtn(false);
                TableInfo.IsOrder = true;
                if (TableInfo.lstOrder.Count == 0)
                {// 전체취소 후 주문
                    App.TableViewModel.TableInfoClear(TableInfo.Number);
                    TableInfo.IsOrder = false;
                    return;
                }
                App.TableViewModel.Order(TableInfo.Number);
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
                App.TableViewModel.AddOrders(food, TableInfo.Number);
            }
            else if (btnName.Equals(ActionOrder.btnOrderSub.ToString()))
            {// -
                App.TableViewModel.AddOrders(food, TableInfo.Number, true);
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
                App.TableViewModel.RemoveOrder(food, TableInfo.Number);
            }
            else if (btnName.Equals(ActionOrder.btnAllCancle.ToString()))
            {// 전체취소
                App.TableViewModel.RemoveOrder(null, TableInfo.Number, true);
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
            if (TableInfo.lstOrder.Count < 0)
            {
                return;
            }

            Pay Pay = Pay.Card;

            if (!(bool)btnCard.IsChecked)
            {
                Pay = Pay.Money;
            }

            string Title = "결제확인";
            App.TableViewModel.Order(TableInfo.Number);
            MessageBoxResult messageBoxResult = MessageBox.Show(Pay + "\n" + TableInfo.Orders, Title, MessageBoxButton.YesNo);

            if((int)messageBoxResult == (int)MessageBoxResult.Yes)
            {
                App.FoodViewModel.Add(TableInfo.lstOrder, Pay);
                this.Visibility = Visibility.Collapsed;
                App.TableViewModel.TableInfoClear(TableInfo.Number);
            }
            return;
        }
    }
}