//  --------------------------------
//  <copyright file="RavenObjectStoreAsyncShould.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>10/01/2014</date>
//  ---------------------------------
namespace DotNetDataTier.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using DotNetDataTier.RavenDb;
    using DotNetDataTier.Tests.RavenDb;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RavenObjectStoreAsyncShould : ObjectStoreAsyncShould
    {
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope",
            Justification = "How the fuck am I supposed to this if I dispose it immediately?")]
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
    }
}