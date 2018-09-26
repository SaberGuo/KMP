using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Xps.Packaging;

namespace KMP.Reporter
{
    public class DefaultDocViewer : DocumentViewer
    {
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var content = ((VisualTreeHelper.GetChild(this, 0) as System.Windows.Controls.Border).Child as Grid);
            var cc = (content.Children[0] as System.Windows.Controls.ContentControl);
            if (cc != null)//工具栏
                cc.Visibility = System.Windows.Visibility.Collapsed;

            var searchBar = content.FindName("PART_FindToolBarHost") as System.Windows.Controls.ContentControl;
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

        public XpsDocument ConvertWordToXPS(string wordDocName)
        {
            FileInfo fi = new FileInfo(wordDocName);
            XpsDocument result = null;
            string xpsDocName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.InternetCache), fi.Name);
            xpsDocName = xpsDocName.Replace(".docx", ".xps").Replace(".doc", ".xps");
            Microsoft.Office.Interop.Word.Application wordApplication = new Microsoft.Office.Interop.Word.Application();
            try
            {
           
                if (File.Exists(xpsDocName))
                {
                    System.IO.File.Delete(xpsDocName);
                    //result = new XpsDocument(xpsDocName, FileAccess.Read);
                }
                wordApplication.Documents.Add(wordDocName);
                Document doc = wordApplication.ActiveDocument;
                doc.ExportAsFixedFormat(xpsDocName, WdExportFormat.wdExportFormatXPS, false, WdExportOptimizeFor.wdExportOptimizeForPrint, WdExportRange.wdExportAllDocument, 0, 0, WdExportItem.wdExportDocumentContent, true, true, WdExportCreateBookmarks.wdExportCreateHeadingBookmarks, true, true, false, Type.Missing);
                result = new XpsDocument(xpsDocName, System.IO.FileAccess.Read);

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                wordApplication.Quit(WdSaveOptions.wdDoNotSaveChanges);
            }

            wordApplication.Quit(WdSaveOptions.wdDoNotSaveChanges);

            return result;
        }

        static void DocumentPathPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            DefaultDocViewer viewer = (sender as DefaultDocViewer);
            if (viewer != null)
            {
                string path = e.NewValue as string;
                if (path != null && System.IO.File.Exists(path))
                {
                    XpsDocument xpsdoc = viewer.ConvertWordToXPS(path);
                   // XpsDocument xpsdoc = new XpsDocument(path, System.IO.FileAccess.Read);
                    viewer.Document = xpsdoc.GetFixedDocumentSequence();
                }

            }
        }

    }
}
