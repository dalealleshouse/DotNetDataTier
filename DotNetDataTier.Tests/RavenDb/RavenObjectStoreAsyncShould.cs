﻿//  --------------------------------
//  <copyright file="RavenObjectStoreAsyncShould.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>10/01/2014</date>
//  ---------------------------------
namespace DotNetDataTier.Tests.RavenDb
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;

    using DotNetDataTier.RavenDb;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Raven.Client;

    [TestClass]
    public class RavenObjectStoreAsyncShould : ObjectStoreAsyncShould
    {
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope",
            Justification = "How the fuck am I supposed to use this if I dispose it immediately?")]
        [TestInitialize]
        public override void Init()
        {
            this.RawData = TestDataGenerator.Generate();
            this.Sut = new RavenObjectStoreAsync<Guid, TestPersistable>(InMemoryDocumentStore.CreateDocumentStore(true), "test");

            foreach (var testPersistable in this.RawData)
            {
                this.Sut.AddAsync(testPersistable).Wait();
            }

            this.Sut.SaveChangesAsync().Wait();
        }

        [TestMethod]
        public async override Task ObjectsQueryWithPredicate()
        {
            var result = this.Sut.ObjectsAsync.Where(t => t.Data == TestDataGenerator.DataStringOne);

            Assert.IsNotNull(result);
            Assert.AreEqual(5, await result.CountAsync());
        }

        [TestMethod]
        public async override Task ObjectsQueryWithoutPredicate()
        {
            var result = this.Sut.ObjectsAsync;

            Assert.IsNotNull(result);
            Assert.AreEqual(10, await result.CountAsync());
        }
    }
}