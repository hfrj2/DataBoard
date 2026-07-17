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
            Loaded += (s, e) =>
            {
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
            };
        }



        private void ComoBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
