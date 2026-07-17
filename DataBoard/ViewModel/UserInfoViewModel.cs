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

namespace DataBoard.ViewModel
{
    public class UserInfoViewModel : ViewModelBase
    {

        //查
        private readonly IProvider<UserInfo> provider = new UserInfoProvider();
        public UserInfoViewModel()
        {
            UserInfos = provider.Select();
        }


        private List<UserInfo> userInfos;
        public List<UserInfo> UserInfos
        {
            get { return userInfos; }
            set { userInfos = value; RaisePropertyChanged(); }
        }

        //添加生产线
        public RelayCommand OpenAddLineWindowCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    var dialog = SimpleIoc.Default.GetInstance<IDialogService>();
                    dialog.ShowMessage("AddUserInfoWindow", "提示");
                    UserInfos = provider.Select();
                });
            }
        }

        //修改生产线
        public RelayCommand<UserInfo> OpenEditUserInfoWindowCommand
        {
            get
            {
                return new RelayCommand<UserInfo>((model) =>
                {
                    var vm = SimpleIoc.Default.GetInstance<EditUserInfoWindowViewModel>();
                    if (vm == null) return;
                    vm.UserInfo = model;
                    var dialog = SimpleIoc.Default.GetInstance<IDialogService>();
                    dialog.ShowMessage("EditUserInfoWindow", "提示");
                    UserInfos = provider.Select();
                });
            }
        }

        // 删除生产线
        public RelayCommand<UserInfo> DeleteUserInfoWindowCommand
        {
            get
            {
                return new RelayCommand<UserInfo>((model) =>
                {
                    if (model == null) return;
                    var dialog = SimpleIoc.Default.GetInstance<IDialogService>();
                    var Task = dialog.ShowMessage("确定要删除吗？", "提示", "", () =>
                    {
                        var count = provider.Delete(model);
                        if (count > 0)
                        {
                            dialog.ShowMessageBox("删除成功", "提示");
                            UserInfos = provider.Select();
                        }

                        else
                        {
                            dialog.ShowMessageBox("删除失败", "提示");
                        }
                    });
                    Task.Start();

                });
            }
        }
    }
}
