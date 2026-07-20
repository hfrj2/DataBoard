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
        private History history;
        public History History
        {
            get { return history; }
            set { history = value; RaisePropertyChanged(); }
        }

        // 可以添加其他编辑相关的属性和命令
        public EditHistoryWindowViewModel()
        {
            // 初始化逻辑
        }
    }
}
