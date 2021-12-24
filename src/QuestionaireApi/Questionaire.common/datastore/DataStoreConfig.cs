namespace Questionaire.common.datastore
{
    public record DataStoreConfig
    {
        /// <summary>
        /// Path to NHibernate configuration file
        /// </summary>
        public string DBConfigFile { get; set; }

        /// <summary>
        /// Path to NHibernate mapping directory
        /// </summary>
        public string DBMappingDir { get; set; }
    }
}
