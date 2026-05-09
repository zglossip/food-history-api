using recipe_catalog_api.DAOs.Interfaces;
using Npgsql;

namespace recipe_catalog_api.DAOs;

public class DatabaseConnectionSupplier : IDatabaseConnectionSupplier
{
    private readonly string _connectionString;

    public DatabaseConnectionSupplier(IConfiguration config, ILogger<DatabaseConnectionSupplier> logger)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection is not configured");

        var builder = new NpgsqlConnectionStringBuilder(_connectionString);
        logger.LogInformation("Connecting to database host={Host} database={Database}",
            builder.Host, builder.Database);
    }

    public string GetConnectionString() => _connectionString;
}