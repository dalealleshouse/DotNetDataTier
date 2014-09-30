//  --------------------------------
//  <copyright file="IDataContext.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>06/01/2014</date>
//  ---------------------------------
namespace DotNetDataTier.EntityFramework
{
    using System.Data.Entity;

    public interface IDataContext : IUnitOfWork
    {
        IDbSet<T> GetSet<T>() where T : class;
    }
}