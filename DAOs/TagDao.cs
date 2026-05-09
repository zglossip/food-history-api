using recipe_catalog_api.DAOs.Interfaces;
using recipe_catalog_api.DAOs.Mappers;
using recipe_catalog_api.DAOs.Util;

using Npgsql;

namespace recipe_catalog_api.DAOs;

public class TagDao(IDatabaseConnectionSupplier databaseConnectionSupplier) : ITagDao
{

    private readonly IDatabaseConnectionSupplier _databaseConnectionSupplier = databaseConnectionSupplier;

    public void Create(List<string> tags, int recipeId)
    {
        string sql = " INSERT INTO recipe_catalog.tag" +
                     " (recipe_id, text)" +
                     " VALUES (@recipeId, @text)";

        tags.ForEach(tag =>
        {
            List<NpgsqlParameter> parameters = new List<NpgsqlParameter>(){
                new NpgsqlParameter("@recipeId", recipeId),
                new NpgsqlParameter("@text", tag)
            };

            DaoUtil.Create(_databaseConnectionSupplier.GetConnectionString(), sql, parameters);
        });
    }

    public void Delete(int recipeId)
    {
        string sql = " DELETE FROM recipe_catalog.tag" +
                     " WHERE recipe_id = @recipeId";

        DaoUtil.Execute(_databaseConnectionSupplier.GetConnectionString(), sql, new List<NpgsqlParameter>() { new NpgsqlParameter("@recipeId", recipeId) });
    }

    public List<string> Get(int recipeId)
    {
        string sql = " SELECT text" +
                     " FROM recipe_catalog.tag" +
                     " WHERE recipe_id = @recipeId";

        return DaoUtil.QueryForList(_databaseConnectionSupplier.GetConnectionString(), sql, new TagMapper(), new List<NpgsqlParameter>() { new NpgsqlParameter("@recipeId", recipeId) });
    }
}