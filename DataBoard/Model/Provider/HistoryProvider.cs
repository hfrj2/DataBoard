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
                var model = db.History.FirstOrDefault(item=>item.Id==t.Id);
                model.LineId=t.LineId;
                model.StopTypeld = t.StopTypeld;
                model.SubLineId=t.SubLineId;
                model.UserInfold = t.UserInfold;
                model.StartTime = t.StartTime;
                model.EndTime= t.EndTime;
                model.Minutes = t.Minutes;
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges();
            }
        }
    }
}
