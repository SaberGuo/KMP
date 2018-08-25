using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
namespace Infranstructure.Tool
{
  public  class CommonTool
    {
        public static bool CheckParameterValue(object obj,out string Message)
        {
            Message = "";
            Type ts = obj.GetType();
            PropertyInfo[] pros = ts.GetProperties();
            foreach (var item in pros)
            {
                object h = item.GetValue(obj, null);
                object[] atts = item.GetCustomAttributes(false);
                DisplayNameAttribute t = (DisplayNameAttribute)atts.Where(a => a.GetType().Name == "DisplayNameAttribute").FirstOrDefault();
                if ( h.GetType() == typeof(double))
                {
                    if ((double)h == 0)
                    {
                       if(t!=null)
                        {
                            Message = t.DisplayName + "值不能为0";
                            return false;
                        }
                       
                    }
                }
                //else
                //{
                //    string message="";
                //   if(! CheckParameterValue(h,out message))
                //    {
                //        if(message==""&&t!=null)
                //        {
                //            Message= t.DisplayName + "值设置错误";
                //        }
                //        else
                //        {
                //            Message = message;
                //        }
                       
                //        return false;
                //    }
                //    return true;
                //}
            }
            return true;
        }
    }
}
