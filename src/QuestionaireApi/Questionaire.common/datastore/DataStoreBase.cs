using System;
using System.IO;
using NHibernate;
using NHibernate.Cfg;

namespace Questionaire.common.datastore
{
    public class DataStoreBase : IDisposable
    {
        protected readonly ISessionFactory sessionFactory;
        protected readonly Configuration dbConfiguration;

        public string DBConfigFile { get; set; }
        public string DBMappingFileDir { get; set; }

        public DataStoreBase(string dbConfigFile, string dbMappingFileDir)
        {
            this.DBConfigFile = dbConfigFile;
            this.DBMappingFileDir = dbMappingFileDir;

            this.dbConfiguration = new Configuration();
            this.dbConfiguration.Configure(this.DBConfigFile);
            this.dbConfiguration.AddDirectory(new DirectoryInfo(this.DBMappingFileDir));
            this.sessionFactory = dbConfiguration.BuildSessionFactory();
        }

        public ISession OpenSession() =>
            this.sessionFactory?.OpenSession();

        public IStatelessSession OpenStatelessSession() =>
            this.sessionFactory?.OpenStatelessSession();

        public void Close()
        {
            this.sessionFactory?.Close();
            this.sessionFactory?.Dispose();
        }

        public void Dispose() =>
            Close();
    }
}
