//  --------------------------------
//  <copyright file="RavenObjectStoreAsync.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>09/30/2014</date>
//  ---------------------------------
namespace DotNetDataTier.RavenDb
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Raven.Client;

    public class RavenObjectStoreAsync<TKey, TEntity> : RavenSessionAsync, IObjectStoreAsync<TKey, TEntity>
        where TEntity : class, IPersistable<TKey>
        where TKey : struct, IEquatable<TKey>
    {
        public RavenObjectStoreAsync(IDocumentStore store, string database)
            : base(store, database)
        {
        }

        public IQueryable<TEntity> ObjectsAsync
        {
            get
            {
                return this.Session.Query<TEntity>();
            }
        }

        public async Task SaveChangesAsync()
        {
            await this.Session.SaveChangesAsync();
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await this.Session.LoadAsync<TEntity>(id);
        }

        public async Task<TEntity> AddAsync(TEntity obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            await this.Session.StoreAsync(obj);
            return obj;
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

            this.Session.Delete(deleteItem);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}