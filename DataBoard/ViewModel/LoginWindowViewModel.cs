using CommonServiceLocator;
using DataBoard.Model;
using DataBoard.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataBoard.ViewModel
{
    public class LoginWindowViewModel : ViewModelBase
    {
        private AppData appData;

        public LoginWindowViewModel()
        {
            appData = ServiceLocator.Current.GetInstance<AppData>();
        }

        public AppData AppData
        {
            get { return appData; }
            set { appData = value; }
        }

        public RelayCommand<LoginWindow> CheckUserCommand2
        {
            get
            {
                var command = new RelayCommand<LoginWindow>((window) =>
                {
                    using (var db = new qwerEntities1())
                    {
                        var model = db.UserInfo.FirstOrDefault(item => item.Name == AppData.CurrentUser.Name && item.Password == AppData.CurrentUser.Password);
                        if (model == null)
                        {

                            MessageBox.Show("用户名或密码错误", "登录失败");
                            return;
                        }
                           
                        AppData.CurrentUser = model;
                    }
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    window.Close();
                });
                return command;
            }
        }
    }
}
