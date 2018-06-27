using KMP.Interface;
using KMP.Interface.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace KMP.Parameterization
{
    [System.Windows.Data.ValueConversion(typeof(IParamedModule), typeof(ImageSource))]
    class PathToImageConvertor : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            IParamedModule param = value as IParamedModule;
            if(param == null)
            {
                return null;
            }
            if(param.PreviewImagePath == null)
            {
                return null;
            }
            return new BitmapImage(new Uri(param.PreviewImagePath));
            
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
