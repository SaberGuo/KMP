using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Xps.Packaging;

namespace KMP.Anlysis
{
    public class DefaultDocViewer : DocumentViewer
    {
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var content = ((VisualTreeHelper.GetChild(this, 0) as Border).Child as Grid);
            var cc = (content.Children[0] as ContentControl);
            if (cc != null)//工具栏
                cc.Visibility = System.Windows.Visibility.Collapsed;

            var searchBar = content.FindName("PART_FindToolBarHost") as ContentControl;
            if (searchBar != null)//搜索栏
                searchBar.Visibility = System.Windows.Visibility.Collapsed;

        }

        public readonly static DependencyProperty DocumentPathDependency = DependencyProperty.Register("DocumentPath",
            typeof(string), typeof(DefaultDocViewer), new PropertyMetadata(new PropertyChangedCallback(DocumentPathPropertyChangedCallback)));
        public string DocumentPath
        {
            get { return (string)GetValue(DocumentPathDependency); }
            set { SetValue(DocumentPathDependency, value); }
        }
        static void DocumentPathPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            DefaultDocViewer viewer = (sender as DefaultDocViewer);
            if (viewer != null)
            {
                string path = e.NewValue as string;
                if(path != null)
                {
                    XpsDocument xpsdoc = new XpsDocument(path, System.IO.FileAccess.Read);
                    viewer.Document = xpsdoc.GetFixedDocumentSequence();
                }
                
            }
        }

    }
}
