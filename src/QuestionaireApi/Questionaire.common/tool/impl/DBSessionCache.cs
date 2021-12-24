using System;
using System.Collections.Concurrent;
using NHibernate;
using Questionaire.common.datastore;

namespace Questionaire.common.tool
{
    public class DataStoreSessionCache : ICache<ISession>, IDisposable
    {
        private readonly DataStoreBase dataStore;
        private readonly ConcurrentDictionary<object, ISession> sessions =
            new ConcurrentDictionary<object, ISession>();

        public DataStoreSessionCache(DataStoreBase dataStore)
        {
            this.dataStore = dataStore;
        }

        public ISession Get(object key) =>
            this.sessions.GetOrAdd(key, this.dataStore.OpenSession());

        public void Clear(object key)
        {
            if (this.sessions.TryRemove(key, out ISession session))
                session.Close();
        }

        public void ClearAll()
        {
            var sessionKeys = this.sessions.Keys;

            foreach (var sessionID in sessionKeys)
                Clear(sessionID);
        }

        public void Dispose() =>
            ClearAll();
    }
}
