//  --------------------------------
//  <copyright file="RavenSessionAsync.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>09/30/2014</date>
//  ---------------------------------
namespace DotNetDataTier.RavenDb
{
    using System;

    using Raven.Client;

    public class RavenSessionAsync : RavenStore, IDisposable
    {
        private IAsyncDocumentSession _asyncDocumentSession;

        protected RavenSessionAsync(IDocumentStore store, string database)
            : base(store, database)
        {
            // The embeddable document store does not support multiple tennets
            this._asyncDocumentSession = this.DoesNotSupportMultipleTennets ? this.Store.OpenAsyncSession() : this.Store.OpenAsyncSession(this.Database);
        }

        protected IAsyncDocumentSession Session
        {
            get
            {
                return this._asyncDocumentSession;
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

            if (this._asyncDocumentSession != null)
            {
                this._asyncDocumentSession.Dispose();
                this._asyncDocumentSession = null;
            }
        }
    }
}