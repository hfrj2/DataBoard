using DataBoard.Entities;
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
        public AppData()
        {
            roleModels = new List<RoleModel>();
            roleModels.Add(new RoleModel { ID=0,Name="管理员"});
            roleModels.Add(new RoleModel { ID = 1, Name = "普通用户" });
        }


        private UserInfo userInfo=new UserInfo() { Name="admin",Password="0"};

        public UserInfo CurrentUser
        {
            get {return userInfo; }
            set{userInfo = value; RaisePropertyChanged(); }
        }

        private List<RoleModel> roleModels;

        public List<RoleModel> RoleModels
        {
            get { return roleModels; }
            set { RoleModels = value;}
        }



    }
}
