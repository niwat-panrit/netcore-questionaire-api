using System;
using System.IO;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Questionaire.common
{
    public class DataStore : IDisposable
    {
        protected Configuration dbConfiguration;
        protected ISessionFactory sessionFactory;
        
        /// <summary>
        /// Instantiate an object of <see cref="QuestionaireDataStore"/>
        /// </summary>
        /// <param name="dbConfigFile">Path to NHibernate config file</param>
        /// <param name="dbMappingDir">Path to NHibernate mapping directory</param>
        public DataStore(string dbConfigFile, string dbMappingDir)
        {
            this.dbConfiguration = new Configuration();
            this.dbConfiguration.Configure(dbConfigFile);
            this.dbConfiguration.AddDirectory(new DirectoryInfo(dbMappingDir));
            this.sessionFactory = this.dbConfiguration.BuildSessionFactory();
        }

        public SchemaExport GetSchemaExport() =>
            new SchemaExport(this.dbConfiguration);

        public ISession OpenSession() =>
            this.sessionFactory.OpenSession();

        public IStatelessSession OpenStatelessSession() =>
            this.sessionFactory.OpenStatelessSession();

        public void Dispose()
        {
            this.sessionFactory.Dispose();
        }
    }
}
