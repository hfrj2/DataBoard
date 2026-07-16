using CommonServiceLocator;
using DataBoard.Model;
using DataBoard.Model.Provider;
using DataBoard.Views;
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
    public class AddSubLineWindowViewModel : ViewModelBase
    {

        private SubLine subline = new SubLine();

        public SubLine SubLine
        {
            get { return subline; }
            set { subline = value; RaisePropertyChanged(); }
        }

        public RelayCommand<Window> AddSubLineCommand
        {
            get
            {
                return new RelayCommand<Window>((window) =>
                {
                    if (string.IsNullOrEmpty(SubLine.Name)) return;
                    if (SubLine.Name.Length > 32) return;
                    var appData = ServiceLocator.Current.GetInstance<AppData>();
                    this.SubLine.UserInfold = appData.CurrentUser.Id;
                    this.SubLine.InsertDate = DateTime.Now;
                    IProvider<SubLine> provider = new SublineProvider();
                    var count = provider.Insert(this.SubLine);
                    if (count > 0)
                    {
                        var dialog = SimpleIoc.Default.GetInstance<IDialogService>();
                        dialog.ShowMessageBox("添加成功", "提示");
                        window.Close();
                        this.SubLine = new SubLine();
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
