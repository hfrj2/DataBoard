using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBoard.Model.Provider
{
    internal class SublineProvider : IProvider<SubLine>
    {
        //删除
        public int Delete(SubLine t)
        {
            using (qwerEntities1 db = new qwerEntities1())

            {
                db.Entry(t).State = System.Data.Entity.EntityState.Deleted;
                return db.SaveChanges();
            }
        }

        //插入
        public int Insert(SubLine t)
        {
            using (qwerEntities1 db = new qwerEntities1())

            {
                db.Entry(t).State = System.Data.Entity.EntityState.Added;
                return db.SaveChanges();
            }
        }

        //查
        public List<SubLine> Select()
        {
            using (qwerEntities1 db = new qwerEntities1())

            {
                return db.SubLine.Include("History").Include("UserInfo").ToList();
            }
        }

        //改
        public int Update(SubLine t)
        {
            using (qwerEntities1 db = new qwerEntities1())
            {
                db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges();
            }
        }
    }
}
