//  --------------------------------
//  <copyright file="IObjectStoreAsync.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>09/30/2014</date>
//  ---------------------------------
namespace DotNetDataTier
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IObjectStoreAsync<TKey, TEntity> : IUnitOfWorkAsync, IDisposable
        where TEntity : class, IPersistable<TKey>
        where TKey : struct, IEquatable<TKey>
    {
        IQueryable<TEntity> ObjectsAsync { get; }

        Task<TEntity> GetByIdAsync(TKey id);

        Task<TEntity> AddAsync(TEntity obj);

        Task DeleteAsync(TEntity obj);
    }
}