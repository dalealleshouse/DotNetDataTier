//  --------------------------------
//  <copyright file="ObjectStoreShould.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>10/01/2014</date>
//  ---------------------------------
namespace DotNetDataTier.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public abstract class ObjectStoreShould : IDisposable
    {
        ~ObjectStoreShould()
        {
            this.Dispose(false);
        }

        protected IEnumerable<TestPersistable> RawData { get; set; }

        protected IObjectStore<Guid, TestPersistable> Sut { get; set; }

        public abstract void Init();

        [TestMethod]
        public void GetByIdReturnNullIfObjectDoesNotExist()
        {
            var result = this.Sut.GetById(Guid.NewGuid());
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetByIdReturnObjectIfItExists()
        {
            var id = this.RawData.First().Id;
            var result = this.Sut.GetById(id);

            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
        }

        [TestMethod]
        public void ObjectsQueryWithPredicate()
        {
            var result = this.Sut.Objects.Where(t => t.Data == TestDataGenerator.DataStringOne);

            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.Count());
        }

        [TestMethod]
        public void ObjectsQueryWithoutPredicate()
        {
            var result = this.Sut.Objects;

            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.Count());
        }

        [TestMethod]
        public void DeleteAnAttachedItem()
        {
            var id = this.RawData.First().Id;
            var deleteItem = this.Sut.GetById(id);

            Assert.AreEqual(10, this.Sut.Objects.Count());
            this.Sut.Delete(deleteItem);
            this.Sut.SaveChanges();
            Assert.AreEqual(9, this.Sut.Objects.Count());

            var item = this.Sut.GetById(id);
            Assert.IsNull(item);
        }

        [TestMethod]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Un",
            Justification = "Fuck CA!!!")]
        public void DeleteAnUnAttachedItem()
        {
            var id = this.RawData.First().Id;
            var deleteItem = new TestPersistable { Id = id };

            Assert.AreEqual(10, this.Sut.Objects.Count());
            this.Sut.Delete(deleteItem);
            this.Sut.SaveChanges();
            Assert.AreEqual(9, this.Sut.Objects.Count());

            var item = this.Sut.GetById(id);
            Assert.IsNull(item);
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void DeleteThrowIfObjectDoesNotExist()
        {
            this.Sut.Delete(new TestPersistable { Id = Guid.NewGuid() });
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void DeleteThrowIfObjectIsNull()
        {
            this.Sut.Delete(null);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void AddThrowsIfNull()
        {
            this.Sut.Add(null);
        }

        [TestMethod]
        public void AddShouldReturnWithNewId()
        {
            var entity = new TestPersistable { Data = "Some Data" };

            var result = this.Sut.Add(entity);

            Assert.IsNotNull(result);
            Assert.AreNotEqual(Guid.Empty, result.Id);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
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