//  --------------------------------
//  <copyright file="IUnitOfWork.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>09/28/2014</date>
//  ---------------------------------
namespace DotNetDataTier
{
    using System.Threading.Tasks;

    public interface IUnitOfWork
    {
        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}