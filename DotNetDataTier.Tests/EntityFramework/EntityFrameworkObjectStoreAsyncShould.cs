//  --------------------------------
//  <copyright file="EntityFrameworkObjectStoreAsyncShould.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>10/01/2014</date>
//  ---------------------------------
namespace DotNetDataTier.Tests.EntityFramework
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using DotNetDataTier.EntityFramework;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EntityFrameworkObjectStoreAsyncShould : ObjectStoreAsyncShould
    {
        [TestInitialize]
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "I need to use the dame object")]
        public override void Init()
        {
            this.RawData = TestDataGenerator.Generate();
            var context = new TestDbContext(Effort.DbConnectionFactory.CreateTransient());
            this.Sut = new EntityFrameworkObjectStoreAsync<Guid, TestPersistable>(context);

            foreach (var testPersistable in this.RawData)
            {
                this.Sut.AddAsync(testPersistable).Wait();
            }

            this.Sut.SaveChangesAsync().Wait();
        }
    }
}