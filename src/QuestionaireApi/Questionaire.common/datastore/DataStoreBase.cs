using System;
using System.IO;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Questionaire.common.datastore
{
    public class DataStoreBase : IDisposable
    {
        protected Configuration dbConfiguration;
        protected ISessionFactory sessionFactory;

        /// <summary>
        /// Instantiate an object of <see cref="DataStoreBase"/>
        /// </summary>
        /// <param name="configuration for NHibernate."></param>
        public DataStoreBase(DataStoreConfig config)
        {
            this.dbConfiguration = new Configuration();
            this.dbConfiguration.Configure(config.DBConfigFile);
            this.dbConfiguration.AddDirectory(new DirectoryInfo(config.DBMappingDir));
            this.sessionFactory = this.dbConfiguration.BuildSessionFactory();
        }

        public SchemaExport GetSchemaExport() =>
            new SchemaExport(this.dbConfiguration);

        public ISession OpenSession() =>
            this.sessionFactory.OpenSession();

        //public IStatelessSession OpenStatelessSession() =>
        //    this.sessionFactory.OpenStatelessSession();

        public void Dispose()
        {
            this.sessionFactory.Dispose();
        }
    }
}
