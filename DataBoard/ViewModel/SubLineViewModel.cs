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
using System.Windows.Shapes;

namespace DataBoard.ViewModel
{
    public class SubLineViewModel : ViewModelBase
    {
        //查
        private IProvider<SubLine> provider = new SubLineProvider();
        public SubLineViewModel()
        {
            sublines = provider.Select();
        }


        private List<SubLine> sublines;
        public List<SubLine> SubLines
        {
            get { return sublines; }
            set { sublines = value; RaisePropertyChanged(); }
        }

        //添加
        public RelayCommand OpenAddSubLineWindowCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    var dialog = SimpleIoc.Default.GetInstance<IDialogService>();
                    dialog.ShowMessage("AddSubLineWindow", "提示");
                    SubLines = provider.Select();
                });
            }
        }

        //修改
        public RelayCommand<SubLine> OpenEditSubLineWindowCommand
        {
            get
            {
                return new RelayCommand<SubLine>((model) =>
                {
                    var vm = SimpleIoc.Default.GetInstance<EditSubLineWindowViewModel>();
                    if (vm == null) return;
                    vm.SubLine = model;
                    var dialog = SimpleIoc.Default.GetInstance<IDialogService>();
                    dialog.ShowMessage("EditSubLineWindow", "提示");
                    SubLines = provider.Select();
                });
            }
        }

        // 删除生产线
        public RelayCommand<SubLine> DeleteSubLineWindowCommand
        {
            get
            {
                return new RelayCommand<SubLine>((model) =>
                {
                    if (model == null) return;
                    var dialog = SimpleIoc.Default.GetInstance<IDialogService>();
                    var Task = dialog.ShowMessage("确定要删除吗？", "提示", "", () =>
                    {
                        var count = provider.Delete(model);
                        if (count > 0)
                        {
                            dialog.ShowMessageBox("删除成功", "提示");
                            SubLines = provider.Select();
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
