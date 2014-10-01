//  --------------------------------
//  <copyright file="IUnitOfWorkAsync.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>09/30/2014</date>
//  ---------------------------------
namespace DotNetDataTier
{
    using System.Threading.Tasks;

    public interface IUnitOfWorkAsync
    {
        Task SaveChangesAsync();
    }
}