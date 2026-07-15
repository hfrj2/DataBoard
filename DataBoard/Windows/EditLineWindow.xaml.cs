using DataBoard.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace DataBoard.Windows
{
    /// <summary>
    /// EditLineWindow.xaml 的交互逻辑
    /// </summary>
    public partial class EditLineWindow : Window
    {
        public EditLineWindow()
        {
            InitializeComponent();
            this.Loaded += (s, e) =>
            {
                var vm = this.DataContext as EditLineWindowViewModel;
                if (vm is null) return;
                vm.Close = () => this.Close();
            };
        }

        private void TextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
