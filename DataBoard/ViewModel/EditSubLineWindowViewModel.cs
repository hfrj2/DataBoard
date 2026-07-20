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
    public class EditSubLineWindowViewModel : ViewModelBase
    {

        public Action Close;

        private SubLine subline;

        public SubLine SubLine
        {
            get { return subline; }
            set { subline = value; RaisePropertyChanged(); }
        }

        public RelayCommand<Window> EditSubLineCommand
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
                    IProvider<SubLine> provider = new SubLineProvider();
                    var count = provider.Update(this.SubLine);
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

        public RelayCommand<System.Windows.Window> DeleteSubLineCommand
        {
            get
            {
                return new RelayCommand<System.Windows.Window>((subline) =>
                {
                    if (subline == null) return;
                    var dialog = SimpleIoc.Default.GetInstance<IDialogService>();
                });
            }
        }
    }
}
