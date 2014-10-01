//  --------------------------------
//  <copyright file="EntityFrameworkObjectStoreAsync.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>09/28/2014</date>
//  ---------------------------------
namespace DotNetDataTier.EntityFramework
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    public class EntityFrameworkObjectStoreAsync<TKey, TEntity> : IObjectStoreAsync<TKey, TEntity>
        where TEntity : class, IPersistable<TKey>
        where TKey : struct, IEquatable<TKey>
    {
        private readonly IDataContext _dataContext;

        public EntityFrameworkObjectStoreAsync(IDataContext dataContext)
        {
            if (dataContext == null)
            {
                throw new ArgumentNullException("dataContext");
            }

            this._dataContext = dataContext;
        }

        ~EntityFrameworkObjectStoreAsync()
        {
            this.Dispose(false);
        }

        public IQueryable<TEntity> ObjectsAsync
        {
            get
            {
                return this._dataContext.GetSet<TEntity>();
            }
        }

        public async Task SaveChangesAsync()
        {
            await this._dataContext.SaveChangesAsync();
        }

        public Task<TEntity> GetByIdAsync(TKey id)
        {
            return this._dataContext.GetSet<TEntity>().FirstOrDefaultAsync(t => t.Id.Equals(id));
        }

        public Task<TEntity> AddAsync(TEntity obj)
        {
            return Task.FromResult(this._dataContext.GetSet<TEntity>().Add(obj));
        }

        public async Task DeleteAsync(TEntity obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            var deleteItem = await this.GetByIdAsync(obj.Id);

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