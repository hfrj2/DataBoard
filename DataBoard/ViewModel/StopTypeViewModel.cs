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
    public class StopTypeViewModel : ViewModelBase
    {

        //查
        private IProvider<StopType> provider = new StopTypeProvider();
        public StopTypeViewModel()
        {
            stoptypes = provider.Select();
        }


        private List<StopType> stoptypes;
        public List<StopType> StopTypes
        {
            get { return stoptypes; }
            set { stoptypes = value; RaisePropertyChanged(); }
        }

        //添加
        public RelayCommand OpenAddStopTypeWindowCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    var dialog = SimpleIoc.Default.GetInstance<IDialogService>();
                    dialog.ShowMessage("AddStopTypeWindow", "提示");
                    StopTypes = provider.Select();
                });
            }
        }

        //修改
        public RelayCommand<StopType> OpenEditStopTypeWindowCommand
        {
            get
            {
                return new RelayCommand<StopType>((model) =>
                {
                    var vm = SimpleIoc.Default.GetInstance<EditStopTypeWindowViewModel>();
                    if (vm == null) return;
                    vm.StopType = model;
                    var dialog = SimpleIoc.Default.GetInstance<IDialogService>();
                    dialog.ShowMessage("EditStopTypeWindow", "提示");
                    StopTypes = provider.Select();
                });
            }
        }

        // 删除生产线
        public RelayCommand<StopType> DeleteStopTypeWindowCommand
        {
            get
            {
                return new RelayCommand<StopType>((model) =>
                {
                    if (model == null) return;
                    var dialog = SimpleIoc.Default.GetInstance<IDialogService>();
                    var Task = dialog.ShowMessage("确定要删除吗？", "提示", "", () =>
                    {
                        var count = provider.Delete(model);
                        if (count > 0)
                        {
                            dialog.ShowMessageBox("删除成功", "提示");
                            StopTypes = provider.Select();
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
