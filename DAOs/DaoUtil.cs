using food_history_api.Models;

using System.Data.SqlClient;

namespace food_history_api.DAOs;

public class DaoUtil
{
    public static void Query(Action<SqlDataReader> action, string connectionString, string sql) => Query(action, connectionString, sql, new List<SqlParameter>());
    public static void Query(Action<SqlDataReader> action, string connectionString, string sql, List<SqlParameter> parameters) 
    {
        using(SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(parameters);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                action.Invoke(reader);
            }

            reader.Close();
        }
    }

    public static void Execute(string connectionString, string sql) => Execute(connectionString, sql, new List<SqlParameter>());

    public static void Execute(string connectionString, string sql, List<SqlParameter> parameters)
    {
        using(SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(parameters);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public static Action<SqlDataReader> GetIngredientListAction(List<Ingredient> ingredients) 
    {
        return reader => ingredients.Add(new Ingredient(reader.GetInt32(reader.GetOrdinal("RECIPE_ID")), 
                                                        reader.GetString(reader.GetOrdinal("NAME")), 
                                                        reader.GetInt32(reader.GetOrdinal("QUANTITY")),
                                                        reader.GetString(reader.GetOrdinal("UOM")), 
                                                        reader.GetString(reader.GetOrdinal("NOTES"))));
    } 

    public static Action<SqlDataReader> GetInstructionListAction(List<string> ingredients) 
    {
        return reader => ingredients.Add(reader.GetString(reader.GetOrdinal("TEXT")));
    }  
}