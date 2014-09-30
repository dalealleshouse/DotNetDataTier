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

    public interface IObjectStore<T> : IUnitOfWork, IDisposable
        where T : class
    {
        IQueryable<T> Objects { get; }

        T Add(T obj);

        T Delete(T obj);
    }
}