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

namespace KMP.Parameterization.PopWindows
{
    /// <summary>
    /// NewModelWin.xaml 的交互逻辑
    /// </summary>
    public partial class NewModelWin : Window
    {
        public NewModelWin()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        private void OKBtnClick(object sender, RoutedEventArgs e)
        {
            this.childWinViewModel.NewModelOKCommand.Execute();
            if (childWinViewModel.NewModelWinState == "Closed")
            {
                this.Visibility = Visibility.Hidden;
            }

        }

        private void CancelBtnClick(object sender, RoutedEventArgs e)
        {
            this.childWinViewModel.NewModelCancelCommand.Execute();
            if (childWinViewModel.NewModelWinState == "Closed")
            {
                this.Visibility = Visibility.Hidden;
            }
        }

        private ChildWinViewModel _cwViewModel;
        public ChildWinViewModel childWinViewModel
        {
            set
            {
                _cwViewModel = value;
                this.DataContext = value;
            }
            get
            {
                return _cwViewModel;
            }
        }
    }
}
