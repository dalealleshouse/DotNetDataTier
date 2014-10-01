//  --------------------------------
//  <copyright file="RavenStore.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>09/30/2014</date>
//  ---------------------------------
namespace DotNetDataTier.RavenDb
{
    using System;

    using Raven.Client;

    public abstract class RavenStore
    {
        private readonly string _database;

        private readonly bool _doesNotSupportMultipleTennets;

        private readonly IDocumentStore _store;

        protected RavenStore(IDocumentStore store, string database)
        {
            if (store == null)
            {
                throw new ArgumentNullException("store");
            }

            if (string.IsNullOrWhiteSpace(database))
            {
                throw new ArgumentNullException("database");
            }

            this._store = store;
            this._database = database;
            this._doesNotSupportMultipleTennets = store.GetType().Name == "EmbeddableDocumentStore";
        }

        protected IDocumentStore Store
        {
            get
            {
                return this._store;
            }
        }

        protected string Database
        {
            get
            {
                return this._database;
            }
        }

        protected bool DoesNotSupportMultipleTennets
        {
            get
            {
                return this._doesNotSupportMultipleTennets;
            }
        }
    }
}