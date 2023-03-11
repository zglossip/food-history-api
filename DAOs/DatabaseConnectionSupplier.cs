using System.Data.SqlClient;

namespace food_history_api.DAOs;

public class DatabaseConnectionSupplier {

    private string connectionString {get;}

    DatabaseConnectionSupplier() {
                IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("connectionsettings.json")
            .Build();

        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        builder.DataSource = config["datasource"];
        builder.InitialCatalog = config["database"];
        builder.UserID = config["username"];
        builder.Password = config["password"];

        connectionString = builder.ConnectionString;
    }
}