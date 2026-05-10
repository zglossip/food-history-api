using recipe_catalog_api.DAOs.Interfaces;
using recipe_catalog_api.DAOs.Mappers;
using recipe_catalog_api.DAOs.Util;

using Npgsql;

namespace recipe_catalog_api.DAOs;

public class TagDao(IDatabaseConnectionSupplier databaseConnectionSupplier) : ITagDao
{

    private readonly IDatabaseConnectionSupplier _databaseConnectionSupplier = databaseConnectionSupplier;

    public async Task CreateAsync(List<string> tags, int recipeId)
    {
        string sql = " INSERT INTO recipe_catalog.tag" +
                     " (recipe_id, text)" +
                     " VALUES (@recipeId, @text)";

        foreach (string tag in tags)
        {
            List<NpgsqlParameter> parameters = new List<NpgsqlParameter>(){
                new NpgsqlParameter("@recipeId", recipeId),
                new NpgsqlParameter("@text", tag)
            };

            await DaoUtil.ExecuteAsync(_databaseConnectionSupplier.GetConnectionString(), sql, parameters);
        }
    }

    public Task DeleteAsync(int recipeId)
    {
        string sql = " DELETE FROM recipe_catalog.tag" +
                     " WHERE recipe_id = @recipeId";

        return DaoUtil.ExecuteAsync(_databaseConnectionSupplier.GetConnectionString(), sql, new List<NpgsqlParameter>() { new NpgsqlParameter("@recipeId", recipeId) });
    }

    public Task<List<string>> GetAsync(int recipeId)
    {
        string sql = " SELECT text" +
                     " FROM recipe_catalog.tag" +
                     " WHERE recipe_id = @recipeId";

        return DaoUtil.QueryForListAsync(_databaseConnectionSupplier.GetConnectionString(), sql, new TagMapper(), new List<NpgsqlParameter>() { new NpgsqlParameter("@recipeId", recipeId) });
    }
}
