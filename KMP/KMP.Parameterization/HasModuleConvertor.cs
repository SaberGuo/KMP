using KMP.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KMP.Parameterization
{
    public class HasModuleConvertor : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            IParamedModule param = value as IParamedModule;
            if (param == null)
            {
                return false;
            }
            else
            {
                return true;
            }
            


        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
