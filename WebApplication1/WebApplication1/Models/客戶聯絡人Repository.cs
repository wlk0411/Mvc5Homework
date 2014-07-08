using System;
using System.Linq;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
    {
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Where(x => !x.IsDelete);
        }

        public void DeleteById( int? id )
        {
            var data = this.Where(x => x.Id == id.Value).FirstOrDefault();
            if( data != null )
            {
                data.IsDelete = true;
                UnitOfWork.Commit();
            }
        }

        public 客戶聯絡人 GetById( int? id )
        {
            return this.All().Where(x => x.Id == id.Value && !x.IsDelete).FirstOrDefault();
        }
    }

    public interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
    {
    }
}