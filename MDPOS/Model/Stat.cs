using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDPOS.Model
{
    public class Stat : INotifyPropertyChanged
    {
        private List<Food> lstorder;
        public List<Food> lstOrder
        {
            get => lstorder;
            set
            {
                lstorder = value;
                OnpropertyChanged(nameof(lstOrder));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnpropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
