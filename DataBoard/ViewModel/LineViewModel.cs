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
    public class LineViewModel : ViewModelBase
    {

	
		private LineProvider lineProvider = new LineProvider();
		public LineViewModel( )
		{
			lines = lineProvider.Select();
		}


        private List<Line> lines;
        public List<Line> Lines
		{
			get { return lines; }
			set { lines = value;RaisePropertyChanged(); }
		}

        //添加生产线
		public RelayCommand OpenAddLineWindowCommand
        {
			get 
			{
				return new RelayCommand(() =>
				{
					var dialog = SimpleIoc.Default.GetInstance<IDialogService>();
					dialog.ShowMessage("AddLineWindow", "提示");
                    Lines = lineProvider.Select();
                });
			}
		}

        //修改生产线
		public RelayCommand<Line> OpenEditLineWindowCommand
		{
            get
            {
                return new RelayCommand<Line>((line) =>
                {
                    var vm = SimpleIoc.Default.GetInstance<EditLineWindowViewModel>();
                    if (vm == null) return;
                    vm.Line = line;
                    var dialog = SimpleIoc.Default.GetInstance<IDialogService>();
                    dialog.ShowMessage("EditLineWindow", "提示");
                    Lines = lineProvider.Select();
                });
            }
        }

        // 删除生产线
        public RelayCommand<Line> DeleteLineWindowCommand
        {
            get
            {
                return new RelayCommand<Line>((model) =>
                {
                    if(model == null) return;
                    var dialog = SimpleIoc.Default.GetInstance<IDialogService>();
                    var Task = dialog.ShowMessage("确定要删除吗？", "提示", "", () =>
                    {
                        var count = lineProvider.Delete(model);
                        if (count > 0)
                        { 
                            dialog.ShowMessageBox("删除成功", "提示");
                            Lines = lineProvider.Select();
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
