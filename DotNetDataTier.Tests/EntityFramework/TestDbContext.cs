//  --------------------------------
//  <copyright file="TestDbContext.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>10/01/2014</date>
//  ---------------------------------
namespace DotNetDataTier.Tests.EntityFramework
{
    using System.Data.Common;
    using System.Data.Entity;
    using System.Threading.Tasks;

    using DotNetDataTier.EntityFramework;

    public class TestDbContext : DbContext, IDataContext
    {
        public TestDbContext(DbConnection connection)
            : base(connection, true)
        {
        }

        public DbSet<TestPersistable> TestPersistables { get; set; }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        public new async Task SaveChangesAsync()
        {
            await base.SaveChangesAsync();
        }

        public IDbSet<T> GetSet<T>() where T : class
        {
            return this.Set<T>();
        }
    }
}