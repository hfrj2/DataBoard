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
        // 防止 OnNowPropertyChangedCallback 与 ComoBox_SelectionChanged 之间递归调用
        private bool _isUpdating;

        public DateTimeControl()
        {
            InitializeComponent();

            for (int i = 1949; i < 2100; i++)
            {
                ComoBoxYear.Items.Add(i);
            }

            for (int i = 1; i < 13; i++)
            {
                ComoBoxMonth.Items.Add(i);
            }

            for (int i = 1; i < 32; i++)
            {
                ComoBoxDay.Items.Add(i);
            }

            for (int i = 0; i < 24; i++)
            {
                ComoBoxHour.Items.Add(i);
            }

            for (int i = 0; i < 60; i++)
            {
                ComoBoxMinute.Items.Add(i);
            }

            for (int i = 0; i < 60; i++)
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
            DependencyProperty.Register(nameof(Now), typeof(DateTime), typeof(DateTimeControl),
                new PropertyMetadata(DateTime.Now, OnNowPropertyChangedCallback));

        private static void OnNowPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is DateTimeControl dateTimeControl)) return;
            if (e.NewValue == null) return;
            if (!(e.NewValue is DateTime dateTime)) return;

            // 防止递归：当 ComoBox_SelectionChanged 正在更新时，跳过本次回调
            if (dateTimeControl._isUpdating) return;

            // SQL datetime only supports 1753-01-01 to 9999-12-31.
            // Default(DateTime) = 0001-01-01 is out of range; treat as unset.
            if (dateTime.Year < 1753)
            {
                dateTime = DateTime.Now;
                dateTimeControl._isUpdating = true;
                dateTimeControl.SetCurrentValue(NowProperty, dateTime);
                dateTimeControl._isUpdating = false;
            }

            dateTimeControl._isUpdating = true;
            dateTimeControl.ComoBoxYear.SelectedItem = dateTime.Year;
            dateTimeControl.ComoBoxMonth.SelectedItem = dateTime.Month;
            dateTimeControl.ComoBoxDay.SelectedItem = dateTime.Day;
            dateTimeControl.ComoBoxHour.SelectedItem = dateTime.Hour;
            dateTimeControl.ComoBoxMinute.SelectedItem = dateTime.Minute;
            dateTimeControl.ComoBoxSecond.SelectedItem = dateTime.Second;
            dateTimeControl._isUpdating = false;
        }

        private void ComoBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 防止递归：当 OnNowPropertyChangedCallback 正在设置控件值时，跳过本次事件
            if (_isUpdating) return;

            if (ComoBoxYear.SelectedItem == null || ComoBoxMonth.SelectedItem == null ||
                ComoBoxDay.SelectedItem == null || ComoBoxHour.SelectedItem == null ||
                ComoBoxMinute.SelectedItem == null || ComoBoxSecond.SelectedItem == null)
                return;

            int y = (int)ComoBoxYear.SelectedItem;
            int mo = (int)ComoBoxMonth.SelectedItem;
            int d = (int)ComoBoxDay.SelectedItem;
            int h = (int)ComoBoxHour.SelectedItem;
            int mi = (int)ComoBoxMinute.SelectedItem;
            int s = (int)ComoBoxSecond.SelectedItem;

            if (y < 1753 || mo < 1 || mo > 12 || d < 1 || d > 31 ||
                h < 0 || h > 23 || mi < 0 || mi > 59 || s < 0 || s > 59)
                return;

            try
            {
                var result = new DateTime(y, mo, d, h, mi, s);
                _isUpdating = true;
                SetCurrentValue(NowProperty, result);
                var be = BindingOperations.GetBindingExpression(this, NowProperty);
                be?.UpdateSource();
                _isUpdating = false;
            }
            catch (ArgumentOutOfRangeException)
            {
                // Invalid date (e.g., Feb 30) — ignore
            }
        }
    }
}
