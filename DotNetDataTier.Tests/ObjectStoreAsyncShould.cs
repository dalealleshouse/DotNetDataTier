﻿//  --------------------------------
//  <copyright file="ObjectStoreAsyncShould.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>10/01/2014</date>
//  ---------------------------------
namespace DotNetDataTier.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public abstract class ObjectStoreAsyncShould : IDisposable
    {
        ~ObjectStoreAsyncShould()
        {
            this.Dispose(false);
        }

        protected IEnumerable<TestPersistable> RawData { get; set; }

        protected IObjectStoreAsync<Guid, TestPersistable> Sut { get; set; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public abstract void Init();

        [TestMethod]
        public async Task GetByIdReturnNullIfObjectDoesNotExist()
        {
            var result = await this.Sut.GetByIdAsync(Guid.NewGuid());
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetByIdReturnObjectIfItExists()
        {
            var id = this.RawData.First().Id;
            var result = await this.Sut.GetByIdAsync(id);

            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
        }

        [TestMethod]
        public virtual async Task ObjectsQueryWithPredicate()
        {
            var result = this.Sut.ObjectsAsync.Where(t => t.Data == TestDataGenerator.DataStringOne);

            Assert.IsNotNull(result);
            Assert.AreEqual(5, await result.CountAsync());
        }

        [TestMethod]
        public async virtual Task ObjectsQueryWithoutPredicate()
        {
            var result = this.Sut.ObjectsAsync;

            Assert.IsNotNull(result);
            Assert.AreEqual(10, await result.CountAsync());
        }

        [TestMethod]
        public async Task DeleteAnAttachedItem()
        {
            var id = this.RawData.First().Id;
            var deleteItem = await this.Sut.GetByIdAsync(id);

            await this.Sut.DeleteAsync(deleteItem);
            await this.Sut.SaveChangesAsync();

            var item = await this.Sut.GetByIdAsync(id);
            Assert.IsNull(item);
        }

        [TestMethod]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Un", Justification = "Fuck CA!!!")]
        public async Task DeleteAnUnAttachedItem()
        {
            var id = this.RawData.First().Id;
            var deleteItem = new TestPersistable { Id = id };

            await this.Sut.DeleteAsync(deleteItem);
            await this.Sut.SaveChangesAsync();

            var item = await this.Sut.GetByIdAsync(id);
            Assert.IsNull(item);
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public async Task DeleteThrowIfObjectDoesNotExist()
        {
            await this.Sut.DeleteAsync(new TestPersistable { Id = Guid.NewGuid() });
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public async Task DeleteThrowIfObjectIsNull()
        {
            await this.Sut.DeleteAsync(null);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public async Task AddThrowsIfNull()
        {
            await this.Sut.AddAsync(null);
        }

        [TestMethod]
        public async Task AddShouldReturnWithNewId()
        {
            var entity = new TestPersistable { Data = "Some Data" };

            var result = await this.Sut.AddAsync(entity);
            await this.Sut.SaveChangesAsync();

            Assert.IsNotNull(result);
            Assert.AreNotEqual(Guid.Empty, result.Id);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.Sut != null)
                {
                    this.Sut.Dispose();
                    this.Sut = null;
                }
            }
        }
    }
}