using food_history_api.Models;

using System.Data.SqlClient;
using Npgsql;

namespace food_history_api.DAOs;

public class DaoUtil
{
    public static void Query(Action<NpgsqlDataReader> action, string connectionString, string sql) => Query(action, connectionString, sql, new List<NpgsqlParameter>());
    public static void Query(Action<NpgsqlDataReader> action, string connectionString, string sql, List<NpgsqlParameter> parameters) 
    {
        using(NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            NpgsqlCommand command = new NpgsqlCommand(sql, connection);

            parameters.ForEach(parameter => command.Parameters.Add(parameter));

            connection.Open();

            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                action.Invoke(reader);
            }

            reader.Close();
        }
    }

    public static void Execute(string connectionString, string sql) => Execute(connectionString, sql, new List<NpgsqlParameter>());

    public static void Execute(string connectionString, string sql, List<NpgsqlParameter> parameters)
    {
        using(NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            command.Parameters.Add(parameters);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public static Action<NpgsqlDataReader> GetIngredientListAction(List<Ingredient> ingredients) 
    {
        return reader => ingredients.Add(new Ingredient(reader.GetInt32(reader.GetOrdinal("RECIPE_ID")), 
                                                        reader.GetString(reader.GetOrdinal("INGREDIENT_NAME")), 
                                                        reader.GetInt32(reader.GetOrdinal("QUANTITY")),
                                                        reader.GetString(reader.GetOrdinal("UOM")), 
                                                        reader.GetString(reader.GetOrdinal("NOTES"))));
    } 

    public static Action<NpgsqlDataReader> GetInstructionListAction(List<string> ingredients) 
    {
        return reader => ingredients.Add(reader.GetString(reader.GetOrdinal("TEXT")));
    }  
}