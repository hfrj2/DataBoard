using DataBoard.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBoard
{
    public class AppData:ViewModelBase
    {
        private UserInfo userInfo=new UserInfo() { Name="admin",Password="0"};

        public UserInfo CurrentUser
        {
            get {return userInfo; }
            set{userInfo = value; RaisePropertyChanged(); }
        }
    }
}
