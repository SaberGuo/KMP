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

namespace KMP.Reporter
{
    [Export(typeof(IReportWindow))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    /// <summary>
    /// ReportWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ReportWindow : Window, IReportWindow
    {
        public ReportWindow()
        {
            InitializeComponent();
        }
        [Import]
        ReportViewModel viewModel
        {
            set
            {
                this.DataContext = value;
            }
            get
            {
                return this.DataContext as ReportViewModel;
            }
        }
        public string Path
        {
            get
            {
                return this.viewModel.GenPath;
            }

            set
            {
                this.viewModel.GenPath = value;
            }
        }

        public IParamedModule Root
        {
            get
            {
                return this.viewModel.Root;
            }

            set
            {
                this.viewModel.Root = value;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }
    }
}
