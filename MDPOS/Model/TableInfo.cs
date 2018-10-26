using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDPOS.Model
{
    public class TableInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int number;
        public int Number
        {
            get => number;
            set
            {
                number = value;
                OnPropertyChanged(nameof(Number));
            }
        }
        private ObservableCollection<Food> lstorder;

        public ObservableCollection<Food> lstOrder
        {
            get
            {
                return lstorder;
            }
            set
            {
                lstorder = value;
                OnPropertyChanged(nameof(lstOrder));
            }
        }

        public int Total { get; set; }
        private DateTime orderTime;
        public DateTime OrderTime
        {
            get => orderTime;
            set
            {
                orderTime = value;
                OnPropertyChanged(nameof(OrderTime));
            }
        }

        private string orders;
        public string Orders
        {
            get
            {
                return orders;
            }
            set
            {
                orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }

        public bool IsOrder { get; set; }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
