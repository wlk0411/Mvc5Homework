using System;
using System.Linq;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
    {
        public override IQueryable<客戶銀行資訊> All()
        {
            return base.All().Where(x => !x.IsDelete);
        }

        public void DeleteById( int? id )
        {
            var data = this.Where(x => x.Id == id.Value).FirstOrDefault();
            if( data != null )
            {
                data.IsDelete = true;
                //UnitOfWork.Commit();
            }
        }

        //public 客戶銀行資訊 GetById( int? id )
        //{
        //    return this.All().Where(x => x.Id == id.Value && !x.IsDelete).FirstOrDefault();
        //}
    }

    public interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
    {
    }
}