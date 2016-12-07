using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;


namespace Scheduler.BusinessLogic
{
    /// <summary>
    /// Implements The IRepository Interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T>:IRepository<T> where T :class
    {
        #region Private Fields
        protected DbSet<T> DbSet;
        private readonly DbContext _dbContext;
        #endregion

        #region Repository Constractor
        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            DbSet = _dbContext.Set<T>();
        }
        public Repository() { }
        #endregion

        #region Implementation of the generic IRepository interface 

        /// <summary>
        /// returns T where T is class(Model) based on the id passed
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(int? id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        /// returns an IQuerable of T where T is class based on the predicate passed
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        /// <summary>
        /// returns a List of T Where T is a Class(Model)
        /// </summary>
        /// <returns></returns>
        public List<T> GetAll()
        {
            return DbSet.ToList();
        }

        /// <summary>
        /// Saves the changes made On T to database where T is a Model
        /// </summary>
        /// <param name="model"></param>
        public void Edit(T model)
        {
            _dbContext.Entry(model).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Saves a new Instance of T to database where T is a Model
        /// </summary>
        /// <param name="model"></param>
        public void Save(T model)
        {
            DbSet.Add(model);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// deletes T from database where T is a Model
        /// </summary>
        /// <param name="className"></param>
        public void Delete(T model)
        {
            DbSet.Remove(model);
            _dbContext.SaveChanges();
        }

        #endregion
    }
}
