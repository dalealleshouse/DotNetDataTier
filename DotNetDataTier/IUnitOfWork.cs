﻿//  --------------------------------
//  <copyright file="IUnitOfWork.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>09/28/2014</date>
//  ---------------------------------
namespace DotNetDataTier
{
    public interface IUnitOfWork
    {
        void SaveChanges();
    }
}