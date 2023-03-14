using food_history_api.Models;
using food_history_api.DAOs.Mappers;

using Npgsql;

namespace food_history_api.DAOs;

public class DaoUtil
{

    public static T Query<T>(string connectionString, string sql, AbstractMapper<T> mapper, List<NpgsqlParameter>? parameters = null)
    {
        List<T> results = QueryForList(connectionString, sql, mapper, parameters);

        if(results.Count() > 1)
        {
            throw new Exception($"More than one result for query: {sql}");
        } else if(results.Count() == 1) {
            return results[0];
        } else {
            return default(T);
        }
    }

    public static List<T> QueryForList<T>(string connectionString, string sql, AbstractMapper<T> mapper, List<NpgsqlParameter>? parameters = null)
    {
        List<T> list = new List<T>();

        _connect(connectionString, sql, parameters, reader => list.Add(mapper.Invoke(reader)));

        return list;
    }

    public static void Execute(string connectionString, string sql,  List<NpgsqlParameter>? parameters = null)
    {
        _connect(connectionString, sql, parameters);
    }

    private static void _connect(string connectionString, string sql, List<NpgsqlParameter>? parameters, Action<NpgsqlDataReader> mapperAction = null) {
        using(NpgsqlConnection connection = new NpgsqlConnection(connectionString)) {
            NpgsqlCommand command = new NpgsqlCommand(sql, connection);

            if(parameters != null)
            {
                parameters.ForEach(parameter => command.Parameters.Add(parameter));
            }

            connection.Open();

            NpgsqlDataReader reader = command.ExecuteReader();

            if(mapperAction != null)
            {
                while (reader.Read())
                {
                    mapperAction.Invoke(reader);
                }
            }
        }
    }

    public static int Create(string connectionString, string sql,  List<NpgsqlParameter>? parameters = null)
    {
        using(NpgsqlConnection connection = new NpgsqlConnection(connectionString)) {
            NpgsqlCommand command = new NpgsqlCommand(sql, connection);

            if(parameters != null)
            {
                parameters.ForEach(parameter => command.Parameters.Add(parameter));
            }

            connection.Open();

            return Convert.ToInt32(command.ExecuteScalar());
        }
    }
}