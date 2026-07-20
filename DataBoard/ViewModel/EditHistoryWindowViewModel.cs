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
    public class EditHistoryWindowViewModel : ViewModelBase
    {
        private IProvider<Line> lineProvider = new LineProvider();
        private IProvider<SubLine> subLineProvider = new SubLineProvider();
        private IProvider<StopType> stoptypeProvider = new StopTypeProvider();
        private IProvider<History> historyProvider = new HistoryProvider();



        public EditHistoryWindowViewModel()
        {
            lines = lineProvider.Select();
            subLines = subLineProvider.Select();
            stopTypes = stoptypeProvider.Select();
        }


        private History history;

        public History History
        {
            get { return history; }
            set
            {
                history = value;
                // 将导航属性按 Id 匹配到下拉列表中的实体对象（不同 DbContext 的引用不相等，需按 Id 匹配）
                if (history != null && lines != null && subLines != null && stopTypes != null)
                {
                    history.Line = lines.FirstOrDefault(l => l.Id == history.LineId);
                    history.SubLine = subLines.FirstOrDefault(s => s.Id == history.SubLineId);
                    history.StopType = stopTypes.FirstOrDefault(s => s.Id == history.StopTypeld);
                }
                RaisePropertyChanged();
            }
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

        //修改历史记录

        public RelayCommand<Window> EditHistoryCommand
        {
            get
            {
                return new RelayCommand<Window>((window) =>
                {
                    if (History.Line == null || history.Line.Id == 0) return;
                    if (History.SubLine == null || history.SubLine.Id == 0) return;
                    if (History.StopType == null || history.StopType.Id == 0) return;
                    if (History.StartTime >= History.EndTime) return;

                    var t = History.EndTime - History.StartTime;
                    this.history.Minute = (int)t.TotalMinutes;
                    this.History.LineId = this.History.Line.Id;
                    this.History.SubLineId = this.History.SubLine.Id;
                    this.History.StopTypeld = this.History.StopType.Id;

                    // 保存前清空导航属性，防止 EF 尝试附加来自其他 DbContext 的实体
                    this.History.Line = null;
                    this.History.SubLine = null;
                    this.History.StopType = null;

                    var appData = ServiceLocator.Current.GetInstance<AppData>();
                    this.History.UserInfold = appData.CurrentUser.Id;


                    var count = historyProvider.Update(this.History);
                    if (count > 0)
                    {
                        var dialog = SimpleIoc.Default.GetInstance<IDialogService>();
                        dialog.ShowMessageBox("修改成功", "提示");
                        window.Close();
                        this.History = new History();
                    }
                    else
                    {
                        var dialog = SimpleIoc.Default.GetInstance<IDialogService>();
                        dialog.ShowMessageBox("修改失败", "提示");

                    }
                });
            }
        }



    }
}
