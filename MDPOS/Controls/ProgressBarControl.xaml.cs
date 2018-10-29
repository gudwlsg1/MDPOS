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
        private const int MAX_VALUE = 100;
        public ProgressBarControl()
        {
            InitializeComponent();

            this.Loaded += ProgressBarControl_Loaded;
        }

        private void ProgressBarControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= ProgressBarControl_Loaded;
            InitProgressBar();
        }

        private void InitProgressBar()
        {
            pbLoading.ValueChanged += PbLoading_ValueChanged;

            Task.Run(() =>
            {
                for (int i = 1; i <= 100; i++)
                {
                    Thread.Sleep(30);
                    this.Dispatcher.Invoke(() =>
                    {
                        pbLoading.Value = i;
                    });
                }
            });
        }

        private void PbLoading_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (pbLoading.Value >= MAX_VALUE)
            {
                this.Visibility = Visibility.Collapsed;
            }
        }
    }
}
