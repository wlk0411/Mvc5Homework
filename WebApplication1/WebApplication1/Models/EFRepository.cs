using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Reflection;

namespace WebApplication1.Models
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        public IUnitOfWork UnitOfWork { get; set; }

        private IDbSet<T> _objectset;
        private IDbSet<T> ObjectSet
        {
            get
            {
                if( _objectset == null )
                {
                    _objectset = UnitOfWork.Context.Set<T>();
                }
                return _objectset;
            }
        }

        public virtual IQueryable<T> All()
        {
            return ObjectSet.AsQueryable();
        }

        public IQueryable<T> Where( Expression<Func<T, bool>> expression )
        {
            return ObjectSet.Where(expression);
        }

        public void Add( T entity )
        {
            ObjectSet.Add(entity);
        }

        public void Delete( T entity )
        {
            ObjectSet.Remove(entity);
        }


        public T GetById( int? id )
        {
            var data = this.All();
            var returnData = default(T);


            PropertyInfo idProp = typeof(T).GetProperty("Id");
            if( idProp != null && id.HasValue )
            {
                //使用 Dynamic LINQ
                returnData = data.Where("Id == @0 AND !IsDelete", id.Value).FirstOrDefault();
            }


            //此寫法會出現錯誤：「具有陳述式主體的 Lambda 運算式無法轉換為運算式樹狀架構」
            //data = data.Where(x =>
            //{
            //    PropertyInfo idProp = typeof(T).GetProperty("id");
            //    if( idProp != null && id.HasValue )
            //    {
            //        var idValue = ( idProp.GetValue(x) ?? string.Empty ).ToString();

            //        int tmp = 0;
            //        int.TryParse(idValue, out tmp);

            //        return ( tmp == id.Value );
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //});
            //return data.FirstOrDefault();


            return returnData;
        }
    }
}