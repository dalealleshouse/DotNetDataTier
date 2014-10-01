//  --------------------------------
//  <copyright file="RavenObjectStoreShould.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>10/01/2014</date>
//  ---------------------------------
namespace DotNetDataTier.Tests.RavenDb
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using DotNetDataTier.RavenDb;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RavenObjectStoreShould : ObjectStoreShould
    {
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope",
            Justification = "How the fuck am I supposed to this if I dispose it immediately?")]
        [TestInitialize]
        public override void Init()
        {
            this.RawData = TestDataGenerator.Generate();
            this.Sut = new RavenObjectStore<Guid, TestPersistable>(InMemoryDocumentStore.CreateDocumentStore(true), "test");

            foreach (var testPersistable in this.RawData)
            {
                this.Sut.Add(testPersistable);
            }

            this.Sut.SaveChanges();
        }
    }
}