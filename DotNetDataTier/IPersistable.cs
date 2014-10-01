//  --------------------------------
//  <copyright file="IPersistable.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>10/01/2014</date>
//  ---------------------------------
namespace DotNetDataTier
{
    using System;

    public interface IPersistable<TKey>
        where TKey : struct, IEquatable<TKey>
    {
        TKey Id { get; set; }
    }
}