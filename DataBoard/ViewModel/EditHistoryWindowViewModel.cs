using DataBoard.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBoard.ViewModel
{
    public class EditHistoryWindowViewModel : ViewModelBase
    {
        public History History { get; internal set; }
    }
}
