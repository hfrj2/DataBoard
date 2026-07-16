using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBoard.Model.Provider
{
    internal class StopTypeProvider:IProvider<StopType>
    {
        //删除
        public int Delete(StopType t)
        {
            using (qwerEntities1 db = new qwerEntities1())

            {
                db.Entry(t).State = System.Data.Entity.EntityState.Deleted;
                return db.SaveChanges();
            }
        }

        //插入
        public int Insert(StopType t)
        {
            using (qwerEntities1 db = new qwerEntities1())

            {
                db.Entry(t).State = System.Data.Entity.EntityState.Added;
                return db.SaveChanges();
            }
        }

        //查
        public List<StopType> Select()
        {
            using (qwerEntities1 db = new qwerEntities1())

            {
                return db.StopType.Include("History").Include("UserInfo").ToList();
            }
        }

        //改
        public int Update(StopType t)
        {
            using (qwerEntities1 db = new qwerEntities1())
            {
                db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges();
            }
        }

    }
}
