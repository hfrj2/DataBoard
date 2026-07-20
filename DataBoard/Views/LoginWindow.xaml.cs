using DataBoard.Windows;
using GalaSoft.MvvmLight.Views;
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

namespace DataBoard.Views
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window,IDialogService
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void TextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void Button_Click()
        {

        }

        public Task ShowError(string message, string title, string buttonText, Action afterHideCallback)
        {
            throw new NotImplementedException();
        }

        public Task ShowError(Exception error, string title, string buttonText, Action afterHideCallback)
        {
            throw new NotImplementedException();
        }

        public Task ShowMessage(string message, string title)
        {
            switch (message)
            {
                case "AddLineWindow": new AddLineWindow().ShowDialog(); break;
                case "EditLineWindow": new EditLineWindow().ShowDialog(); break;

                case "AddSubLineWindow": new AddSubLineWindow().ShowDialog(); break;
                case "EditSubLineWindow": new EditSubLineWindow().ShowDialog(); break;
                case "AddStopTypeWindow": new AddStopTypeWindow().ShowDialog(); break;
                case "EditStopTypeWindow": new EditStopTypeWindow().ShowDialog(); break;
                case "AddUserInfoWindow": new AddUserInfoWindow().ShowDialog(); break;
                case "EditUserInfoWindow": new EditUserInfoWindow().ShowDialog(); break;
                case "AddHistoryWindow": new AddHistoryWindow().ShowDialog(); break;
                case "EditHistoryWindow": new EditHistoryWindow().ShowDialog(); break;

                default:
                    break;
            }
            return null;
        }

        public Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback)
        {
            var result = MessageBox.Show(message, title, MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
                return new Task(afterHideCallback);
            else
                return new Task(() => { });
        }

        public Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText, Action<bool> afterHideCallback)
        {
            throw new NotImplementedException();
        }

        public Task ShowMessageBox(string message, string title)
        {
            MessageBox.Show(message, title);
            return null;
        }
    }
}
