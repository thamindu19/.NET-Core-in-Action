using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScmDataAccess;
using SqliteDal;

namespace SqliteScmTest {
  public class SampleScmDataFixture {
    private const string PartTypeTable = 
        @"CREATE TABLE PartType(
            Id INTEGER PRIMARY KEY,
            Name VARCHAR(255) NOT NULL
          );";
    private const string InventoryItemTable =
        @"CREATE TABLE InventoryItem(
            PartTypeId INTEGER PRIMARY KEY,
            Count INTEGER NOT NULL,
            OrderThreshold INTEGER,
            FOREIGN KEY(PartTypeId) REFERENCES PartType(Id)
          );";
    private const string SupplierTable =
        @"CREATE TABLE Supplier(
            Id INTEGER PRIMARY KEY,
            Name VARCHAR(255) NOT NULL,
            Email VARCHAR(255) NOT NULL,
            PartTypeId INTEGER NOT NULL,
            FOREIGN KEY(PartTypeId) REFERENCES PartType(Id)
          );";
    private const string OrderTable =
        @"CREATE TABLE [Order](
            Id INTEGER PRIMARY KEY,
            SupplierId INTEGER NOT NULL,
            PartTypeId INTEGER NOT NULL,
            PartCount INTEGER NOT NULL,
            PlacedDate DATETIME NOT NULL,
            FulfilledDate DATETIME,
            FOREIGN KEY(SupplierId) REFERENCES Supplier(Id),
            FOREIGN KEY(PartTypeId) REFERENCES PartType(Id)
          );";
    private const string SendEmailCommandTable =
        @"CREATE TABLE SendEmailCommand(
            Id INTEGER PRIMARY KEY,
            [To] VARCHAR(255) NOT NULL,
            Subject VARCHAR(255) NOT NULL,
            Body BLOB
          );";
    const string ConnStrKey = "ConnectionString";
    // in case the config file isn't there
    const string DefConnStr = "Data Source=:memory:";

    static Dictionary<string, string> Config {get;} = 
      new Dictionary<string, string>() {
        // Adds a key with the name ConnectionString
        [ConnStrKey] = DefConnStr
      };

    // This is how DI is exposed to the tests
    public IServiceProvider Services
      { get; private set; }
    
    public SampleScmDataFixture() {
      // var conn = new SqliteConnection(
      // "Data Source=:memory:");
      // conn.Open();
      // (new SqliteCommand(PartTypeTable, conn)).ExecuteNonQuery();
      // var serviceCollection = new ServiceCollection();
      // IScmContext context = new SqliteScmContext(conn);
      // serviceCollection.AddSingleton<IScmContext>(context);
      // Services = serviceCollection.BuildServiceProvider();
      var configBuilder = new ConfigurationBuilder();
      // coding style - method chaining or fluent interface
      configBuilder
        .AddInMemoryCollection(Config) // Serves as the default, last one with a value wins
        .AddJsonFile("config.json", true);
      var configRoot = configBuilder.Build();
      // Retrieves the connection string given the key
      var connStr = configRoot[ConnStrKey];
      var serviceCollection = new ServiceCollection();
      serviceCollection.AddTransient<IScmContext>(provider => {
        var conn = new SqliteConnection(connStr);
        conn.Open();

        (new SqliteCommand(PartTypeTable, conn)).ExecuteNonQuery();
        (new SqliteCommand(InventoryItemTable, conn)).ExecuteNonQuery();
        (new SqliteCommand(SupplierTable, conn)).ExecuteNonQuery();
        (new SqliteCommand(OrderTable, conn)).ExecuteNonQuery();
        (new SqliteCommand(SendEmailCommandTable, conn)).ExecuteNonQuery();
        var command = new SqliteCommand(
          @"INSERT INTO PartType
              (Id, Name)
              VALUES
              (0, '8289 L-shaped plate')",
          conn);
        command.ExecuteNonQuery();
        command = new SqliteCommand(
          @"INSERT INTO InventoryItem
              (PartTypeId, Count, OrderThreshold)
              VALUES
              (0, 100, 10)",
          conn);
        command.ExecuteNonQuery();
        command = new SqliteCommand(
          @"INSERT INTO Supplier
              (Name, Email, PartTypeId)
              VALUES
              ('Joe Supplier', 'joe@joesupplier.com', 0)",
          conn);
        command.ExecuteNonQuery();

        return new SqliteScmContext(conn);
      });
      Services = serviceCollection.BuildServiceProvider();
    }
  }
}
