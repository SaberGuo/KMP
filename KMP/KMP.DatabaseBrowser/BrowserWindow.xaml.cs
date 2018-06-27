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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KMP.DatabaseBrowser
{

    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    /// 
    [Export(typeof(IBrowserWindow))]
    public partial class BrowserWindow : Window, IBrowserWindow
    {
        public BrowserWindow()
        {
            InitializeComponent();
        }

        private BrowserViewModel _viewModel;
        [Import]
        BrowserViewModel viewModel
        {
            set {
                this.DataContext = value;
                this._viewModel = value;
            }
            get
            {
                return this._viewModel;
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        private void WareHouseEnvironment_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            this.viewModel.ProjectTypeChanged("WareHouseEnvironment");
        }

        private void ContainerSystem_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            this.viewModel.ProjectTypeChanged("ContainerSystem");
        }

        private void HeaterSystem_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            this.viewModel.ProjectTypeChanged("HeaterSystem");
        }
    }
}
