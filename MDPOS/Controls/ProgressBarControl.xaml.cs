using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace MDPOS.Controls
{
    /// <summary>
    /// Interaction logic for ProgressBarControl.xaml
    /// </summary>
    public partial class ProgressBarControl : UserControl
    {
        public ProgressBarControl()
        {
            InitializeComponent();

            this.Loaded += ProgressBarControl_Loaded;
        }

        private void ProgressBarControl_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                for (int i = 1; i <= 100; i++)
                {
                    Thread.Sleep(30);
                    this.Dispatcher.Invoke(() =>
                    {
                        pbLoading.Value = i;
                        if(pbLoading.Value >= 100)
                        {
                            this.Visibility = Visibility.Collapsed;
                        }
                    });
                }
            });
        }
    }
}
