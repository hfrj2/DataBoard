using DataBoard.Model;
using DataBoard.Model.Provider;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBoard.ViewModel
{
    public class AddHistoryWindowViewModel : ViewModelBase
    {
        private IProvider<Line> lineProvider = new LineProvider();
        private IProvider<SubLine> subLineProvider = new SubLineProvider();
        private IProvider<StopType> stoptypeProvider = new StopTypeProvider();
       
        public AddHistoryWindowViewModel()
        {
            lines = lineProvider.Select();
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
            get { return SubLines; }
            set { SubLines = value; RaisePropertyChanged(); }
        }


        private List<StopType> stopTypes;
        public List<StopType> StopTypes
        {
            get { return stopTypes; }
            set { stopTypes = value; RaisePropertyChanged(); }
        }

    }
}
