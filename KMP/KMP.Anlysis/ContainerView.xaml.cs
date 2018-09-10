using System;
using System.Collections.Generic;
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
using System.Windows.Xps.Packaging;

namespace KMP.Anlysis
{
    /// <summary>
    /// CantainerView.xaml 的交互逻辑
    /// </summary>
    public partial class ContainerView : UserControl
    {
        public ContainerView()
        {
            InitializeComponent();
            //string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "preview\\容器系统计算.xps");
            //XpsDocument doc = new XpsDocument(path, System.IO.FileAccess.Read);
            //docviewer.Document = doc.GetFixedDocumentSequence();
        }

        
    }
}
