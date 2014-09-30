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
    using System.Threading.Tasks;

    public class EntityFrameworkObjectStore<T> : IObjectStore<T>
        where T : class
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

        public IQueryable<T> Objects
        {
            get
            {
                return this._dataContext.GetSet<T>();
            }
        }

        public int SaveChanges()
        {
            return this._dataContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this._dataContext.SaveChangesAsync();
        }

        public T Add(T obj)
        {
            return this._dataContext.GetSet<T>().Add(obj);
        }

        public T Delete(T obj)
        {
            return this._dataContext.GetSet<T>().Remove(obj);
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