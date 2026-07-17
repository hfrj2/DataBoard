using CommonServiceLocator;
using DataBoard.Model;
using DataBoard.Model.Provider;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataBoard.ViewModel
{
    public class EditUserInfoWindowViewModel : ViewModelBase
    {

        public Action Close;

        private UserInfo userInfo;

        public UserInfo UserInfo
        {
            get { return userInfo; }
            set { userInfo = value; RaisePropertyChanged(); }
        }

        public RelayCommand<Window> EditUserInfoCommand
        {
            get
            {
                return new RelayCommand<Window>((window) =>
                {
                    if (string.IsNullOrEmpty(UserInfo.Name)) return;
                    if (string.IsNullOrEmpty(UserInfo.Password)) return;
                    if (UserInfo.Name.Length > 32) return;
                    if (UserInfo.Password.Length > 32) return;


                    IProvider<UserInfo> provider = new UserInfoProvider();
                    var count = provider.Update(this.UserInfo);
                    if (count > 0)
                    {
                        var dialog = SimpleIoc.Default.GetInstance<IDialogService>();
                        dialog.ShowMessageBox("修改成功", "提示");
                        window?.Close();
                    }
                    else
                    {
                        var dialog = SimpleIoc.Default.GetInstance<IDialogService>();
                        dialog.ShowMessageBox("修改失败", "提示");

                    }
                });
            }
        }

        public RelayCommand<System.Windows.Window> DeleteUserInfoCommand
        {
            get
            {
                return new RelayCommand<System.Windows.Window>((userInfo) =>
                {
                    if (userInfo == null) return;
                    var dialog = SimpleIoc.Default.GetInstance<IDialogService>();
                });
            }
        }
    }
}
