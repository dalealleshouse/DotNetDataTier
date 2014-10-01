//  --------------------------------
//  <copyright file="IObjectStore.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>09/28/2014</date>
//  ---------------------------------
namespace DotNetDataTier
{
    using System;
    using System.Linq;

    public interface IObjectStore<TKey, TEntity> : IUnitOfWork, IDisposable
        where TEntity : class, IPersistable<TKey>
        where TKey : struct, IEquatable<TKey>
    {
        IQueryable<TEntity> Objects { get; }

        TEntity GetById(TKey id);

        TEntity Add(TEntity obj);

        void Delete(TEntity obj);
    }
}