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
                // 如果 Id 为 0（默认值），手动获取下一个可用 Id
                // 防止数据库列未设置 IDENTITY 时插入失败
                if (t.Id == 0)
                {
                    var maxId = db.UserInfo.Max(u => (int?)u.Id) ?? 0;
                    t.Id = maxId + 1;
                }
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
