//  --------------------------------
//  <copyright file="RavenObjectStore.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>09/30/2014</date>
//  ---------------------------------
namespace DotNetDataTier.RavenDb
{
    using System;
    using System.Linq;

    using Raven.Client;

    public class RavenObjectStore<TKey, TEntity> : RavenSession, IObjectStore<TKey, TEntity>
        where TEntity : class, IPersistable<TKey>
        where TKey : struct, IEquatable<TKey>
    {
        public RavenObjectStore(IDocumentStore store, string database)
            : base(store, database)
        {
        }

        public IQueryable<TEntity> Objects
        {
            get
            {
                return this.Session.Query<TEntity>();
            }
        }

        public TEntity GetById(TKey id)
        {
            return this.Session.Load<TEntity>(id);
        }

        public TEntity Add(TEntity obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            this.Session.Store(obj);
            return obj;
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

            this.Session.Delete(deleteItem);
        }

        public void SaveChanges()
        {
            this.Session.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}