using KMP.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Xps.Packaging;
using System.Xml;

namespace KMP.Anlysis
{
    [Export(typeof(IAnalysisWindow))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    /// <summary>
    /// AnlysisWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AnlysisWindow : Window, IAnalysisWindow
    {
        public AnlysisWindow()
        {
            InitializeComponent();
            //XpsDocument p = new XpsDocument("d:\\myWord.xps", System.IO.FileAccess.Read);
            //documentViewer.Document = p.GetFixedDocumentSequence();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        [Import]
        AnalysisViewModel viewModel
        {
            get
            {
                return this.DataContext as AnalysisViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }

        public IParamedModule BaseModule
        {
            set
            {
                this.viewModel.BaseModule = value;
            }
        }
    }
}
