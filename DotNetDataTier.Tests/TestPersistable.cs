//  --------------------------------
//  <copyright file="TestPersistable.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>10/01/2014</date>
//  ---------------------------------
namespace DotNetDataTier.Tests
{
    using System;

    public class TestPersistable : IPersistable<Guid>
    {
        public string Data { get; set; }

        public Guid Id { get; set; }
    }
}