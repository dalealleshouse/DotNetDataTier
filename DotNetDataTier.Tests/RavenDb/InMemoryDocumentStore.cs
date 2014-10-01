//  --------------------------------
//  <copyright file="InMemoryDocumentStore.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>09/30/2014</date>
//  ---------------------------------
namespace DotNetDataTier.Tests.RavenDb
{
    using System.Diagnostics.CodeAnalysis;

    using Raven.Client;
    using Raven.Client.Document;
    using Raven.Client.Embedded;

    public static class InMemoryDocumentStore
    {
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope",
            Justification = "How the fuck am I supposed to this if I dispose it immediately?")]
        public static IDocumentStore CreateDocumentStore(bool forceFreshIndexes = false)
        {
            var memoryStore = new EmbeddableDocumentStore { RunInMemory = true };

            if (forceFreshIndexes)
            {
                memoryStore.Conventions.DefaultQueryingConsistency = ConsistencyOptions.AlwaysWaitForNonStaleResultsAsOfLastWrite;
            }

            memoryStore.Initialize();

            return memoryStore;
        }
    }
}