using CommonServiceLocator;
using DataBoard.Model;
using DataBoard.Model.Provider;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using System;

namespace DataBoard.ViewModel
{
    public class EditLineWindowViewModel : ViewModelBase
    {
        public Action Close;

        private Line line;

        public Line Line
        {
            get { return line; }
            set { line = value; RaisePropertyChanged(); }
        }

        public RelayCommand EditLineCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (string.IsNullOrEmpty(line.Name)) return;
                    if (line.Name.Length > 32) return;
                    var appData = ServiceLocator.Current.GetInstance<AppData>();
                    this.Line.UserInfoId = appData.CurrentUser.Id;
                    this.Line.InsertDate = DateTime.Now;
                    LineProvider lineProvider = new LineProvider();
                    var count = lineProvider.Update(this.Line);
                    if (count > 0)
                    {
                        var dialog = SimpleIoc.Default.GetInstance<IDialogService>();
                        dialog.ShowMessageBox("修改成功", "提示");
                        Close?.Invoke();
                    }
                    else
                    {
                        var dialog = SimpleIoc.Default.GetInstance<IDialogService>();
                        dialog.ShowMessageBox("修改失败", "提示");

                    }
                });
            }
        }

        public RelayCommand<System.Windows.Window> DeleteLineCommand
        {
            get
            {
                return new RelayCommand<System.Windows.Window>((line) =>
                {
                    if (line == null) return;
                    var dialog = SimpleIoc.Default.GetInstance<IDialogService>();
                });
            }
        }

    }
}


