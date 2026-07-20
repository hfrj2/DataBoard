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
            int year,month, day, hour, minute, second;
            year=dateTime.Year;
            month=dateTime.Month;
            day=dateTime.Day; 
            hour=dateTime.Hour;
            minute=dateTime.Minute; 
            second=dateTime.Second;
            dateTimeControl.ComoBoxYear.Text=year.ToString();
            dateTimeControl.ComoBoxMonth.Text = month.ToString();
            dateTimeControl.ComoBoxDay.Text = day.ToString();
            dateTimeControl.ComoBoxHour.Text = hour.ToString();
            dateTimeControl.ComoBoxMinute.Text = minute.ToString();
            dateTimeControl.ComoBoxSecond.Text = second.ToString();
        }

        private void ComoBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var year = ComoBoxYear.Text;
            var month = ComoBoxMonth.Text;
            var day = ComoBoxDay.Text;
            var hour = ComoBoxHour.Text;
            var minute = ComoBoxMinute.Text;
            var second = ComoBoxSecond.Text;
            var datetime = $"{year}/{month}/{day}/{hour}/{minute}/{second}";
            if (DateTime.TryParse(datetime,out DateTime result))
            {
                SetCurrentValue(NowProperty, result);
                var be = BindingOperations.GetBindingExpression(this, NowProperty);
                be?.UpdateSource();

            }
        }
    }
}
