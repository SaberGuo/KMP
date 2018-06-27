using Infranstructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Infranstructure.Tool
{
    public class ModelConverter: ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context,
                                   System.Type destinationType)
        {
            if (destinationType == typeof(User))
                return true;
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture,
    object value, System.Type destinationType)
        {
            if (destinationType == typeof(System.String) &&
                 value is User)
            {
                User so = (User)value;
                return so.Name;
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
                    string s = (string)value;

                    User so = new User();
                    so.Name = s;
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
