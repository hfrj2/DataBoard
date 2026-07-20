using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataBoard.Controls
{
    /// <summary>
    /// DateTimeControl.xaml 的交互逻辑
    /// </summary>
    public partial class DateTimeControl : UserControl
    {
        public DateTimeControl()
        {
            InitializeComponent();
            //
            for (int i = 1949; i < 2100; i++)
            {
                ComoBoxYear.Items.Add(i);
            }

            //
            for (int i = 1; i < 13; i++)
            {
                ComoBoxMonth.Items.Add(i);
            }

            //
            for (int i = 1; i < 32; i++)
            {
                ComoBoxDay.Items.Add(i);
            }

            //
            for (int i = 0; i < 25; i++)
            {
                ComoBoxHour.Items.Add(i);
            }

            //
            for (int i = 0; i < 61; i++)
            {
                ComoBoxMinute.Items.Add(i);
            }

            //
            for (int i = 0; i < 61; i++)
            {
                ComoBoxSecond.Items.Add(i);
            }
        }





        public DateTime Now
        {
            get { return (DateTime)GetValue(NowProperty); }
            set { SetValue(NowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Now.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NowProperty =
            DependencyProperty.Register(nameof(Now), typeof(DateTime), typeof(DateTimeControl), new PropertyMetadata(DateTime.Now,OnNowPropertyChangedCallback));

        private static void OnNowPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is DateTimeControl dateTimeControl)) return;
            if (e.NewValue==null) return;
            if (!(e.NewValue is DateTime dateTime)) return;

            // SQL datetime only supports 1753-01-01 to 9999-12-31.
            // Default(DateTime) = 0001-01-01 is out of range; treat as unset.
            if (dateTime.Year < 1753)
            {
                dateTime = DateTime.Now;
                dateTimeControl.SetCurrentValue(NowProperty, dateTime);
            }

            dateTimeControl.ComoBoxYear.Text = dateTime.Year.ToString();
            dateTimeControl.ComoBoxMonth.Text = dateTime.Month.ToString();
            dateTimeControl.ComoBoxDay.Text = dateTime.Day.ToString();
            dateTimeControl.ComoBoxHour.Text = dateTime.Hour.ToString();
            dateTimeControl.ComoBoxMinute.Text = dateTime.Minute.ToString();
            dateTimeControl.ComoBoxSecond.Text = dateTime.Second.ToString();
        }

        private void ComoBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var year = ComoBoxYear.Text;
            var month = ComoBoxMonth.Text;
            var day = ComoBoxDay.Text;
            var hour = ComoBoxHour.Text;
            var minute = ComoBoxMinute.Text;
            var second = ComoBoxSecond.Text;

            if (int.TryParse(year, out int y) &&
                int.TryParse(month, out int mo) &&
                int.TryParse(day, out int d) &&
                int.TryParse(hour, out int h) &&
                int.TryParse(minute, out int mi) &&
                int.TryParse(second, out int s) &&
                y >= 1753 && mo >= 1 && mo <= 12 && d >= 1 && d <= 31 &&
                h >= 0 && h <= 23 && mi >= 0 && mi <= 59 && s >= 0 && s <= 59)
            {
                try
                {
                    var result = new DateTime(y, mo, d, h, mi, s);
                    SetCurrentValue(NowProperty, result);
                    var be = BindingOperations.GetBindingExpression(this, NowProperty);
                    be?.UpdateSource();
                }
                catch (ArgumentOutOfRangeException)
                {
                    // Invalid date (e.g., Feb 30) — ignore
                }
            }
        }
    }
}
