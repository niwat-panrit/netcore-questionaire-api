using System.IO;
using System.Reflection;
using NUnit.Framework;
using Questionaire.common;

namespace Questionnaire.commom.Tests;

public class QuestionaireDataStoreTests
{
    private string dbConfigFile = string.Empty;
    private string dbMappingDir = string.Empty;

    [SetUp]
    public void Setup()
    {
        var currentDir = Directory.GetCurrentDirectory();
        this.dbConfigFile = Path.Combine(currentDir, "questionnaire.hibernate.cfg.xml.xml");
        System.Console.WriteLine($"Testing with config file {this.dbConfigFile}");
        this.dbMappingDir = Path.Combine(currentDir, "mapping");
        System.Console.WriteLine($"Testing with mapping dir {this.dbMappingDir}");
    }

    [Test]
    public void Instantiate_ConfigsCorrect_DataStoreBuilt()
    {
        var dataStore = new DataStore(
            this.dbConfigFile,
            this.dbMappingDir);

        Assert.NotNull(dataStore);
    }

    [Test]
    public void GetSchemaExport_ConfigsCorrect_SchemaExportCreated()
    {
        // Arrange
        var dataStore = new DataStore(
            this.dbConfigFile,
            this.dbMappingDir);

        // Act
        var schemaExport = dataStore.GetSchemaExport();

        // Assert
        Assert.NotNull(schemaExport);
    }

    [Test]
    public void OpenSession_ConfigsCorrect_SessionOpenedAndConnected()
    {
        var dataStore = new DataStore(
            this.dbConfigFile,
            this.dbMappingDir);

        var session = dataStore.OpenSession();

        Assert.NotNull(session);
        Assert.AreEqual(true, session.IsOpen);
        Assert.AreEqual(true, session.IsConnected);
    }

    [Test]
    public void OpenStatelessSession_ConfigsCorrect_StatelessSessionOpenedAndConnected()
    {
        var dataStore = new DataStore(
            this.dbConfigFile,
            this.dbMappingDir);

        var session = dataStore.OpenStatelessSession();

        Assert.NotNull(session);
        Assert.AreEqual(true, session.IsOpen);
        Assert.AreEqual(true, session.IsConnected);
    }
}
