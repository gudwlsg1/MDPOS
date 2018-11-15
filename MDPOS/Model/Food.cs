using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDPOS.Model
{
    public enum Category
    {
        김밥류,
        식사류,
        분식류,
        계절음식,
        음료
    }

    public enum Pay
    {
        Card,
        Money
    }

    public class Food : INotifyPropertyChanged, ICloneable
    {
        public Category Category { get; set; }

        private string name;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                Picture = @"/Resources/" + name + ".jpg";
                OnPropertyChanged(nameof(Name));
            }
        }
        private int price;
        public int Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
                OnPropertyChanged(nameof(Price));
            }
        }
        private int count;
        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                count = value;
                OnPropertyChanged(nameof(Count));
            }
        }
        public string Picture { get; set; }

        public string Barcode { get; set; }

        private int total;
        public int Total
        {
            get => total;
            set
            {
                total = value;
                OnPropertyChanged(nameof(Total));
            }
        }

        public Pay Pay { get; set; }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public object Clone()
        {
            Food food = new Food();
            food.Name = this.Name;
            food.Category = this.Category;
            food.Count = this.Count;
            food.Total = this.Total;
            food.Picture = this.Picture;
            food.Price = this.Price;
            food.Pay = this.Pay;

            return food;
        }
    }
}
