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
    public class AddStopTypeWindowViewModel : ViewModelBase
    {

        private StopType stoptype = new StopType();

        public StopType StopType
        {
            get { return stoptype; }
            set { stoptype = value; RaisePropertyChanged(); }
        }

        public RelayCommand<Window> AddStopTypeCommand
        {
            get
            {
                return new RelayCommand<Window>((window) =>
                {
                    if (string.IsNullOrEmpty(StopType.Name)) return;
                    if (StopType.Name.Length > 32) return;
                    var appData = ServiceLocator.Current.GetInstance<AppData>();
                    this.StopType.UserInfoId = appData.CurrentUser.Id;
                    this.StopType.InsertDate = DateTime.Now;
                    IProvider<StopType> provider = new StopTypeProvider();

                    var count = provider.Insert(this.StopType);
                    if (count > 0)
                    {
                        var dialog = SimpleIoc.Default.GetInstance<IDialogService>();
                        dialog.ShowMessageBox("添加成功", "提示");
                        window.Close();
                        this.StopType = new StopType();
                    }
                    else
                    {
                        var dialog = SimpleIoc.Default.GetInstance<IDialogService>();
                        dialog.ShowMessageBox("添加失败", "提示");

                    }
                });
            }
        }
    }
}
