using System.IO;
using NUnit.Framework;
using Questionaire.common.datastore;

namespace Questionnaire.commom.Tests;

public class DataStoreBaseTests
{
    class DBConfig : DataStoreConfig
    {
        public string DBConfigFile { get; init; } = string.Empty;

        public string DBMappingDir { get; init; } = string.Empty;
    }

    private DataStoreConfig? dbConfig;

    [SetUp]
    public void Setup()
    {
        System.Console.WriteLine("Setting up config file and dir paths...");
        var currentDir = Directory.GetCurrentDirectory();
        this.dbConfig = new DBConfig()
        {
            DBConfigFile = Path.Combine(currentDir, "questionnaire.hibernate.cfg.xml.xml"),
            DBMappingDir = Path.Combine(currentDir, "mapping"),
        };
        System.Console.WriteLine($"Testing with config file {this.dbConfig.DBConfigFile}");
        System.Console.WriteLine($"Testing with mapping dir {this.dbConfig.DBMappingDir}");   
    }

    [Test]
    public void Instantiate_ConfigsCorrect_DataStoreBuilt()
    {
        var dataStore = new DataStoreBase(this.dbConfig);

        Assert.NotNull(dataStore);
    }

    [Test]
    public void GetSchemaExport_ConfigsCorrect_SchemaExportCreated()
    {
        // Arrange
        var dataStore = new DataStoreBase(this.dbConfig);

        // Act
        var schemaExport = dataStore.GetSchemaExport();

        // Assert
        Assert.NotNull(schemaExport);
    }

    [Test]
    public void OpenSession_ConfigsCorrect_SessionOpenedAndConnected()
    {
        var dataStore = new DataStoreBase(this.dbConfig);

        var session = dataStore.OpenSession();

        Assert.NotNull(session);
        Assert.AreEqual(true, session.IsOpen);
        Assert.AreEqual(true, session.IsConnected);
    }

    [Test]
    public void OpenStatelessSession_ConfigsCorrect_StatelessSessionOpenedAndConnected()
    {
        var dataStore = new DataStoreBase(this.dbConfig);

        var session = dataStore.OpenStatelessSession();

        Assert.NotNull(session);
        Assert.AreEqual(true, session.IsOpen);
        Assert.AreEqual(true, session.IsConnected);
    }
}
