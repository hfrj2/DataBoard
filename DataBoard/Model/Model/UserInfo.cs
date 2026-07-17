using DataBoard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBoard.Model
{
    public partial class UserInfo
    {
        public RoleModel RoleModel { get; set; } = new RoleModel();
    }
}
