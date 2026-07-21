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
    public class HistoryViewModel : ViewModelBase
    {

        //查
        private readonly IProvider<History> provider = new HistoryProvider();
        public HistoryViewModel()
        {
            Historys = provider.Select();
        }


        private List<History> histories;
        public List<History> Historys
        {
            get { return histories; }
            set { histories = value; RaisePropertyChanged(); }
        }

        //添加生产线
        public RelayCommand OpenAddHistoryWindowCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    var dialog = SimpleIoc.Default.GetInstance<IDialogService>();
                    dialog.ShowMessage("AddHistoryWindow", "提示");
                    Historys = provider.Select();
                });
            }
        }

        //修改生产线
        public RelayCommand<History> OpenEditHistoryWindowCommand
        {
            get
            {
                return new RelayCommand<History>((model) =>
                {
                    var vm = SimpleIoc.Default.GetInstance<EditHistoryWindowViewModel>();
                    if (vm == null) return;
                    vm.History = model;
                    var dialog = SimpleIoc.Default.GetInstance<IDialogService>();
                    dialog.ShowMessage("EditHistoryWindow", "提示");
                    Historys = provider.Select();
                });
            }
        }

        // 删除生产线
        public RelayCommand<History> DeleteHistoryWindowCommand
        {
            get
            {
                return new RelayCommand<History>((model) =>
                {
                    if (model == null) return;
                    var dialog = SimpleIoc.Default.GetInstance<IDialogService>();
                    var Task = dialog.ShowMessage("确定要删除吗？", "提示", "", () =>
                    {
                        var count = provider.Delete(model);
                        if (count > 0)
                        {
                            dialog.ShowMessageBox("删除成功", "提示");
                            Historys = provider.Select();
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
