using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DataBoard.Converter
{
    public class RoleConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           if (value == null) return "未知";
            if (value.ToString() == "0")
                return "管理员";
            else
                return "普通用户";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return -1;
            if (value.ToString() == "管理员")
                return 0;
            else
                return 1;
        }
    }
}
