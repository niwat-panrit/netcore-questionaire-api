using System;
using System.IO;
using NHibernate;
using NHibernate.Cfg;

namespace Questionaire.common.datastore
{
    public class DataStoreBase : IDisposable
    {
        protected ISessionFactory sessionFactory;
        protected Configuration dbConfiguration;

        public string DBConfigFile { get; }
        public string DBMappingFileDir { get; }

        public DataStoreBase()
        {
            var currentDir = Directory.GetCurrentDirectory();
            this.DBConfigFile = Path.Combine(currentDir, "Configs", "questionnaire.hibernate.cfg.xml");
            this.DBMappingFileDir = Path.Combine(currentDir, "mapping");

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
