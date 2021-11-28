namespace Questionaire.common.datastore
{
    public interface IDataStoreConfig
    {
        string DBConfigFile { get; }

        string DBMappingDir { get; }
    }
}

