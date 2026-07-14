using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBoard.Model.Provider
{
    public class LineProvider : IProvider<Line>
    {
        public int Delete(Line t)
        {
            throw new NotImplementedException();
        }

        public int Insert(Line t)
        {
            using (qwerEntities1 db = new qwerEntities1())

            {
                db.Entry(t).State = System.Data.Entity.EntityState.Added;
                return db.SaveChanges();
            }
        }

        public List<Line> Select()
        {
            using (qwerEntities1 db = new qwerEntities1())

            {
                return db.Line.Include("UserInfo").ToList();
            }
        }

        public int Update(Line t)
        {
            throw new NotImplementedException();
        }
    }
}
