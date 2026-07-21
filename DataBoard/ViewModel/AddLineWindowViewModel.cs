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
    public class AddLineWindowViewModel:ViewModelBase
    {
        private IProvider<Line> lineProvider = new LineProvider();

        private Line line = new Line();

        public Line Line
        {
            get { return line; }
            set { line = value;RaisePropertyChanged(); }
        }

        public RelayCommand<Window> AddLineCommand 
        {
        get
            {
                return new RelayCommand<Window>((window) =>
                    {
                        if(string.IsNullOrEmpty(line.Name)) return;
                        if (line.Name.Length > 32) return;
                        var appData = ServiceLocator.Current.GetInstance<AppData>();
                        this.Line.UserInfoId = appData.CurrentUser.Id;
                        this.Line.InsertDate = DateTime.Now;
                        LineProvider lineProvider = new LineProvider();
                        var count = lineProvider.Insert(this.Line);
                        if(count>0)
                        {
                            var dialog = SimpleIoc.Default.GetInstance<IDialogService>();
                            dialog.ShowMessageBox("添加成功", "提示");
                            window.Close();
                            this.Line=new Line();
                          
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
