using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBoard.Model.Provider
{
    public class UserInfoProvider:IProvider<UserInfo>
    {
        public int Delete(UserInfo t)
        {
            using (qwerEntities1 db = new qwerEntities1())

            {
                db.Entry(t).State = System.Data.Entity.EntityState.Deleted;
                return db.SaveChanges();
            }
        }

        //插入
        public int Insert(UserInfo t)
        {
            using (qwerEntities1 db = new qwerEntities1())

            {
                db.Entry(t).State = System.Data.Entity.EntityState.Added;
                return db.SaveChanges();
            }
        }

        //查
        public List<UserInfo> Select()
        {
            using (qwerEntities1 db = new qwerEntities1())

            {
                return db.UserInfo
                    .Include("SubLine")
                    .Include("StopType")
                    .Include("Line")
                    .Include("History").ToList();
            }
        }

        //改
        public int Update(UserInfo t)
        {
            using (qwerEntities1 db = new qwerEntities1())
            {
                db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges();
            }
        }
    }
}
