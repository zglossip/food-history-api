using food_history_api.DAOs.Interfaces;

using System.Data.SqlClient;

namespace food_history_api.DAOs;

public class DatabaseConnectionSupplier : IDatabaseConnectionSupplier{

    private readonly string _connectionString;

    public DatabaseConnectionSupplier()
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("connectionsettings.json")
            .Build();

        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        builder.DataSource = config["datasource"];
        builder.InitialCatalog = config["database"];
        builder.UserID = config["username"];
        builder.Password = config["password"];

        _connectionString = builder.ConnectionString;
    }

    public string GetConnectionString()
    {
        return _connectionString;
    }
}