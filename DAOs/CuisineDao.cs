using recipe_catalog_api.DAOs.Interfaces;
using recipe_catalog_api.DAOs.Mappers;
using recipe_catalog_api.DAOs.Util;

using Npgsql;

namespace recipe_catalog_api.DAOs;

public class CuisineDao(IDatabaseConnectionSupplier databaseConnectionSupplier) : ICuisineDao
{

    private readonly IDatabaseConnectionSupplier _databaseConnectionSupplier = databaseConnectionSupplier;

    public async Task CreateAsync(List<string> cuisines, int recipeId)
    {
        string sql = " INSERT INTO recipe_catalog.cuisine" +
                     " (recipe_id, text)" +
                     " VALUES (@recipeId, @text)";

        foreach (string cuisine in cuisines)
        {
            List<NpgsqlParameter> parameters = new List<NpgsqlParameter>(){
                new NpgsqlParameter("@recipeId", recipeId),
                new NpgsqlParameter("@text", cuisine)
            };

            await DaoUtil.ExecuteAsync(_databaseConnectionSupplier.GetConnectionString(), sql, parameters);
        }
    }

    public Task DeleteAsync(int recipeId)
    {
        string sql = " DELETE FROM recipe_catalog.cuisine" +
                     " WHERE recipe_id = @recipeId";

        return DaoUtil.ExecuteAsync(_databaseConnectionSupplier.GetConnectionString(), sql, new List<NpgsqlParameter>() { new NpgsqlParameter("@recipeId", recipeId) });
    }

    public Task<List<string>> GetAsync(int recipeId)
    {
        string sql = " SELECT text" +
                     " FROM recipe_catalog.cuisine" +
                     " WHERE recipe_id = @recipeId";

        return DaoUtil.QueryForListAsync(_databaseConnectionSupplier.GetConnectionString(), sql, new CuisineMapper(), new List<NpgsqlParameter>() { new NpgsqlParameter("@recipeId", recipeId) });
    }
}
