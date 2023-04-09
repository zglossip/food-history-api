using food_history_api.DAOs.Interfaces;
using food_history_api.DAOs.Mappers;
using food_history_api.DAOs.Util;

using Npgsql;

namespace food_history_api.DAOs;

public class CuisineDao : ICusineDao
{

    private readonly IDatabaseConnectionSupplier _databaseConnectionSupplier;

    public CuisineDao(IDatabaseConnectionSupplier databaseConnectionSupplier)
    {
        _databaseConnectionSupplier = databaseConnectionSupplier;
    }

    public void Create(List<string> cusisines, int recipeId)
    {
        string sql = " INSERT INTO food_history.cuisine" +
                     " (recipe_id, text)" +
                     " VALUES (@recipeId, @text)";

        cusisines.ForEach(cuisine => {
            List<NpgsqlParameter> parameters = new List<NpgsqlParameter>(){
                new NpgsqlParameter("@recipeId", recipeId),
                new NpgsqlParameter("@text", cuisine)
            };

            DaoUtil.Create(_databaseConnectionSupplier.GetConnectionString(), sql, parameters);
        });
    }

    public void Delete(int recipeId)
    {
        string sql = " DELETE FROM food_history.cuisine" +
                     " WHERE recipe_id = @recipeId";

        DaoUtil.Execute(_databaseConnectionSupplier.GetConnectionString(), sql, new List<NpgsqlParameter>(){new NpgsqlParameter("@recipeId", recipeId)});
    }

    public List<string> Get(int recipeId)
    {
        string sql = " SELECT text" +
                     " FROM food_history.cuisine" +
                     " WHERE recipe_id = @recipeId";

        return DaoUtil.QueryForList(_databaseConnectionSupplier.GetConnectionString(), sql, new CuisineMapper(), new List<NpgsqlParameter>(){new NpgsqlParameter("@recipeId", recipeId)});
    }
}