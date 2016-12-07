using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
namespace Scheduler.BusinessLogic
{
    public interface IRepository<T>
    {
       
        T GetById(int? id);
        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);
        List<T> GetAll();
        void Edit(T model);
        void Save(T model);
        void Delete(T model);
    }
        
}
