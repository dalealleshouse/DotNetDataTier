//  --------------------------------
//  <copyright file="RavenSession.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>09/30/2014</date>
//  ---------------------------------
namespace DotNetDataTier.RavenDb
{
    using System;

    using Raven.Client;

    public abstract class RavenSession : RavenStore, IDisposable
    {
        private IDocumentSession _documentSession;

        protected RavenSession(IDocumentStore store, string database)
            : base(store, database)
        {
            // The embeddable document store does not support multiple tennets
            this._documentSession = this.DoesNotSupportMultipleTennets ? this.Store.OpenSession() : this.Store.OpenSession(this.Database);
        }

        protected IDocumentSession Session
        {
            get
            {
                return this._documentSession;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (this._documentSession != null)
            {
                this._documentSession.Dispose();
                this._documentSession = null;
            }
        }
    }
}