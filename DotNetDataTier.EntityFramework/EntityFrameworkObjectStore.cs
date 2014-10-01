//  --------------------------------
//  <copyright file="EntityFrameworkObjectStore.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>09/28/2014</date>
//  ---------------------------------
namespace DotNetDataTier.EntityFramework
{
    using System;
    using System.Linq;

    public class EntityFrameworkObjectStore<TKey, TEntity> : IObjectStore<TKey, TEntity>
        where TEntity : class, IPersistable<TKey>
        where TKey : struct, IEquatable<TKey>
    {
        private readonly IDataContext _dataContext;

        public EntityFrameworkObjectStore(IDataContext dataContext)
        {
            if (dataContext == null)
            {
                throw new ArgumentNullException("dataContext");
            }

            this._dataContext = dataContext;
        }

        ~EntityFrameworkObjectStore()
        {
            this.Dispose(false);
        }

        public IQueryable<TEntity> Objects
        {
            get
            {
                return this._dataContext.GetSet<TEntity>();
            }
        }

        public void SaveChanges()
        {
            this._dataContext.SaveChanges();
        }

        public TEntity GetById(TKey id)
        {
            return this._dataContext.GetSet<TEntity>().FirstOrDefault(t => t.Id.Equals(id));
        }

        public TEntity Add(TEntity obj)
        {
            return this._dataContext.GetSet<TEntity>().Add(obj);
        }

        public void Delete(TEntity obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            var deleteItem = this.GetById(obj.Id);

            if (deleteItem == null)
            {
                throw new InvalidOperationException("The item does not exist");
            }

            this._dataContext.GetSet<TEntity>().Remove(deleteItem);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // ReSharper disable once SuspiciousTypeConversion.Global
                var disposable = this._dataContext as IDisposable;

                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
        }
    }
}