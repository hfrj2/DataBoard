using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;


namespace DataBoard.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {

            appData = ServiceLocator.Current.GetInstance<AppData>();
            Task.Run(() =>
            {
                while (true)
                {
                    this.SystemTime = DateTime.Now.ToString();
                    Thread.Sleep(1000);
                }
            });
        }

        private AppData appData;

        public AppData AppData
        {
            get { return appData; }
            set{ appData = value; }
        }

        private string systemTime;

        public string SystemTime
        {
            get { return systemTime; }
            set { systemTime = value;RaisePropertyChanged(); }
        }

        public RelayCommand ExitCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    System.Windows.Application.Current.Shutdown();
                });
            }
        }


        internal void Show()
        {
            throw new NotImplementedException();
        }
    }
}