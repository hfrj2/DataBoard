using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBoard.Model.Provider
{
    public class LineProvider : IProvider<Line>
    {
        //删除
        public int Delete(Line t)
        {
            using (qwerEntities1 db = new qwerEntities1())

            {
                db.Entry(t).State = System.Data.Entity.EntityState.Deleted;
                return db.SaveChanges();
            }
        }

        //插入
        public int Insert(Line t)
        {
            using (qwerEntities1 db = new qwerEntities1())

            {
                db.Entry(t).State = System.Data.Entity.EntityState.Added;
                return db.SaveChanges();
            }
        }

        //查
        public List<Line> Select()
        {
            using (qwerEntities1 db = new qwerEntities1())

            {
                return db.Line.Include("UserInfo").ToList();
            }
        }

        //改
        public int Update(Line t)
        {
            using (qwerEntities1 db = new qwerEntities1())
            {
                db.Entry(t).State=System.Data.Entity.EntityState.Modified;
                return db.SaveChanges();
            }
        }
    }
}
