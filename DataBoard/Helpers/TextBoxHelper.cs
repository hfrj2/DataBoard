using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataBoard.Helpers
{
    public class TextBoxHelper
    {
        // 提示文字（Placeholder）
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.RegisterAttached(
                "Placeholder",
                typeof(string),
                typeof(TextBoxHelper),
                new PropertyMetadata("请输入"));

        public static string GetPlaceholder(DependencyObject obj)
        {
            return (string)obj.GetValue(PlaceholderProperty);
        }

        public static void SetPlaceholder(DependencyObject obj, string value)
        {
            obj.SetValue(PlaceholderProperty, value);
        }

        // 图标（Icon）
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.RegisterAttached(
                "Icon",
                typeof(object),
                typeof(TextBoxHelper),
                new PropertyMetadata(null));

        public static object GetIcon(DependencyObject obj)
        {
            return obj.GetValue(IconProperty);
        }

        public static void SetIcon(DependencyObject obj, object value)
        {
            obj.SetValue(IconProperty, value);
        }

        // 图标宽度
        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.RegisterAttached(
                "IconWidth",
                typeof(double),
                typeof(TextBoxHelper),
                new PropertyMetadata(20.0));

        public static double GetIconWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(IconWidthProperty);
        }

        public static void SetIconWidth(DependencyObject obj, double value)
        {
            obj.SetValue(IconWidthProperty, value);
        }

        // 图标高度
        public static readonly DependencyProperty IconHeightProperty =
            DependencyProperty.RegisterAttached(
                "IconHeight",
                typeof(double),
                typeof(TextBoxHelper),
                new PropertyMetadata(20.0));

        public static double GetIconHeight(DependencyObject obj)
        {
            return (double)obj.GetValue(IconHeightProperty);
        }

        public static void SetIconHeight(DependencyObject obj, double value)
        {
            obj.SetValue(IconHeightProperty, value);
        }
    }
}
