//  --------------------------------
//  <copyright file="TestDataGenerator.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>10/01/2014</date>
//  ---------------------------------
namespace DotNetDataTier.Tests
{
    using System;
    using System.Collections.Generic;

    internal static class TestDataGenerator
    {
        public static string DataStringOne
        {
            get
            {
                return "Data One";
            }
        }

        public static string DataStringTwo
        {
            get
            {
                return "Data Two";
            }
        }

        public static IEnumerable<TestPersistable> Generate()
        {
            var data = new List<TestPersistable>();

            for (var i = 0; i < 10; i++)
            {
                data.Add(new TestPersistable { Id = Guid.NewGuid(), Data = (i % 2 == 0) ? DataStringOne : DataStringTwo });
            }

            return data;
        }
    }
}