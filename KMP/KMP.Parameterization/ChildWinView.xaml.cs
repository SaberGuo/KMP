using Infranstructure.Behaviors;
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

namespace KMP.Parameterization
{
    [ViewExport(RegionName = RegionNames.ChildWinRegion)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    /// <summary>
    /// ChildWinView.xaml 的交互逻辑
    /// </summary>
    public partial class ChildWinView : UserControl
    {
        public ChildWinView()
        {
            InitializeComponent();
        }

        [Import]
        MainDockViewModel _viewModel
        {
            set {
                this.DataContext = ((MainDockViewModel)value).ChildWinViewModel;
           

            }
        }

    }
}
