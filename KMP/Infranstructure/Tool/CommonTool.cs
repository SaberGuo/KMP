using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
namespace Infranstructure.Tool
{
  public  class CommonTool
    {
        public static bool CheckParameterValue(object obj)
        {
            Type ts = obj.GetType();
            PropertyInfo[] pros = ts.GetProperties();
            foreach (var item in pros)
            {
                object h = item.GetValue(obj, null);
                if (h != null && h.GetType() == typeof(double))
                {
                    if ((double)h == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
