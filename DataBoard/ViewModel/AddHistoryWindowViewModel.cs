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
    public class AddHistoryWindowViewModel : ViewModelBase
    {
        private IProvider<Line> lineProvider = new LineProvider();
        private IProvider<SubLine> subLineProvider = new SubLineProvider();
        private IProvider<StopType> stoptypeProvider = new StopTypeProvider();
        private IProvider<History> historyProvider = new HistoryProvider();



        public AddHistoryWindowViewModel()
        {
            lines = lineProvider.Select();
            subLines = subLineProvider.Select();
            stopTypes = stoptypeProvider.Select();
        }


   private History history = new History() { StartTime = DateTime.Now, EndTime = DateTime.Now };

        public History History
        {
            get { return history; }
            set { history = value; RaisePropertyChanged(); }
        }


        private List<Line> lines;
        public List<Line> Lines
        {
            get { return lines; }
            set { lines = value; RaisePropertyChanged(); }
        }

        private List<SubLine> subLines;
        public List<SubLine> SubLines
        {
            get { return subLines; }
            set { subLines = value; RaisePropertyChanged(); }
        }


        private List<StopType> stopTypes;
        public List<StopType> StopTypes
        {
            get { return stopTypes; }
            set { stopTypes = value; RaisePropertyChanged(); }
        }

        //添加历史记录

        public RelayCommand<Window> AddHistoryCommand
        {
            get
            {
                return new RelayCommand<Window>((window) =>
                {
                    if (History.Line == null || history.Line.Id == 0) return;
                    if (History.SubLine == null || history.SubLine.Id == 0) return;
                    if (History.StopType == null || history.StopType.Id == 0) return;
                    if (History.StartTime == null || history.EndTime == null) return;
                    this.History.LineId = this.History.Line.Id;
                    this.History.SubLineId = this.History.SubLine.Id;
                    this.History.StopTypeld = this.History.StopType.Id;
                    
                    this.History.Line = null;
                    this.History.SubLine = null;
                    this.History.StopType = null;
                    


                    var appData = ServiceLocator.Current.GetInstance<AppData>();
                    this.History.UserInfold = appData.CurrentUser.Id;
                    this.History.InsertDate = DateTime.Now;

                   
                    var count = historyProvider.Insert(this.History);
                    if (count > 0)
                    {
                        var dialog = SimpleIoc.Default.GetInstance<IDialogService>();
                        dialog.ShowMessageBox("添加成功", "提示");
                        window.Close();
                        this.History = new History() { StartTime = DateTime.Now, EndTime = DateTime.Now };
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
