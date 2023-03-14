using food_history_api.DAOs.Interfaces;

using Npgsql;

namespace food_history_api.DAOs;

public class DatabaseConnectionSupplier : IDatabaseConnectionSupplier{

    private readonly string _connectionString;
    private readonly ILogger<DatabaseConnectionSupplier> _logger;

    public DatabaseConnectionSupplier(ILogger<DatabaseConnectionSupplier> logger)
    {
        _logger = logger;
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("connectionsettings.json")
            .Build();

        NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder();
            builder.Host = config["host"];
            if(config["port"] != null) {
                builder.Port = int.Parse(config["port"]);
            }
            builder.Database = config["database"];
            builder.Username = config["username"];
            builder.Password = config["password"];

        _logger.LogInformation($"Connecting to database: {builder.ConnectionString}");

        _connectionString = builder.ConnectionString;
    }

    public string GetConnectionString()
    {
        return _connectionString;
    }
}