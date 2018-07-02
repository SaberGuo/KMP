using KMP.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KMP.Anlysis
{
    [Export(typeof(IAnalysisWindow))]
    /// <summary>
    /// AnlysisWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AnlysisWindow : Window, IAnalysisWindow
    {
        public AnlysisWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        [Import]
        AnalysisViewModel viewModel
        {
            set
            {
                this.DataContext = value;
            }
        }
    }
}
