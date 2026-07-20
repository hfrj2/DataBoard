using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBoard.Model.Provider
{
    public class HistoryProvider : IProvider<History>
    {
        //删除
        public int Delete(History t)
        {
            using (qwerEntities1 db = new qwerEntities1())

            {
                db.Entry(t).State = System.Data.Entity.EntityState.Deleted;
                return db.SaveChanges();
            }
        }

        //插入
        public int Insert(History t)
        {
            using (qwerEntities1 db = new qwerEntities1())

            {
                db.Entry(t).State = System.Data.Entity.EntityState.Added;
                return db.SaveChanges();
            }
        }

        //查
        public List<History> Select()
        {
            using (qwerEntities1 db = new qwerEntities1())

            {
                return db.History
                    .Include("Line")
                    .Include("StopType")
                    .Include("SubLine")
                    .Include("UserInfo")
                    .ToList();
            }
        }

        //改
        public int Update(History t)
        {
            using (qwerEntities1 db = new qwerEntities1())
            {
                db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges();
            }
        }
    }
}
