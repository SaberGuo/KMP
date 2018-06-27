using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace KMP.DatabaseBrowser
{
    [System.Windows.Data.ValueConversion(typeof(string), typeof(ImageSource))]
    class PathToThumbnailConvertor: System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string path = value as string;

            if(path != null && System.IO.File.Exists(path))
            {
                string dir = System.IO.Path.GetDirectoryName(path);
                string name = System.IO.Path.GetFileNameWithoutExtension(path);
                string npath = System.IO.Path.Combine(dir, name) + ".iam";
                if (System.IO.File.Exists(npath))
                {
                    return ShellFile.FromFilePath(npath).Thumbnail.BitmapSource;
                }
                
            }

            return new BitmapImage(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "default.png")));

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
