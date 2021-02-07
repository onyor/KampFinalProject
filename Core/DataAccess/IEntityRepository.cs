using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{

    // Generic Constraint
    // class: referans tip olabilir!
    // IEntity olabilir, yada IEentity implement'e eden bir nesne olabilir.
    // new(): new'lene bilir olmalı.
    public interface IEntityRepository<T> where T:class,IEntity, new()
    {
        //Filter=null ise tüm data'nın listelenmesini istiyoruzdur
        List<T> GetAll(Expression<Func<T,bool>> filter=null);
        T Get(Expression<Func<T,bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
