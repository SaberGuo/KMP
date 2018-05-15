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

namespace KMP.ToolBar
{
    /// <summary>
    /// MainToolBarView.xaml 的交互逻辑
    /// </summary>
    /// 
    [ViewExport(RegionName = RegionNames.MainToolBarRegion)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class MainToolBarView : UserControl
    {
        public MainToolBarView()
        {
            InitializeComponent();

            
        }
    }
}
