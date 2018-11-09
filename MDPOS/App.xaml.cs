using MDPOS.Model;
using MDPOS.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MDPOS
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        public static FoodViewModel FoodViewModel = new FoodViewModel();
        public static TableViewModel TableViewModel = new TableViewModel();
    }
}
