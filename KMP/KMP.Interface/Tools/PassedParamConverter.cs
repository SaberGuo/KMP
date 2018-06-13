using KMP.Interface.Model.Container;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using KMP.Interface.Model;
namespace KMP.Interface.Tools
{
    public class PassedParamConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context,
                                   System.Type destinationType)
        {
            if (destinationType == typeof(PassedParameter))
                return true;
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture,
    object value, System.Type destinationType)
        {
            if (destinationType == typeof(System.Double) &&
                 value is PassedParameter)
            {
                PassedParameter so = (PassedParameter)value;
                return so.Value;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
        public override bool CanConvertFrom(ITypeDescriptorContext context, System.Type sourceType)
        {
            if (sourceType == typeof(double))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is double)
            {
                try
                {
                    double s = (double)value;

                    PassedParameter so = new PassedParameter();
                    so.Value = s;
                    return so;
                    
                }
                catch
                {
                    throw new ArgumentException(
                        "无法将“" + (string)value +
                                           "”转换为 PassedParameter 类型");
                }
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}
