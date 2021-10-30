using System;
using System.IO;
using NHibernate;
using NHibernate.Cfg;

namespace Questionaire.common.datastore
{
    public class DataStoreBase : IDisposable
    {
        public static string Directory => Path.GetDirectoryName(
            typeof(DataStoreBase).Assembly.Location);

        protected ISessionFactory sessionFactory;
        protected Configuration dbConfiguration;

        public string DBConfigFile { get; }
        public string DBMappingFileDir { get; }

        public DataStoreBase()
        {
            this.DBConfigFile = Path.Combine(DataStoreBase.Directory, "Configs", "questionnaire.hibernate.cfg.xml");
            this.DBMappingFileDir = Path.Combine(DataStoreBase.Directory, "mapping");

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
