using recipe_catalog_api.DAOs.Interfaces;
using recipe_catalog_api.DAOs.Mappers;
using recipe_catalog_api.DAOs.Util;

using Npgsql;

namespace recipe_catalog_api.DAOs;

public class CourseDao(IDatabaseConnectionSupplier databaseConnectionSupplier) : ICourseDao
{

    private readonly IDatabaseConnectionSupplier _databaseConnectionSupplier = databaseConnectionSupplier;

    public async Task CreateAsync(List<string> courses, int recipeId)
    {
        string sql = " INSERT INTO recipe_catalog.course" +
                     " (recipe_id, text)" +
                     " VALUES (@recipeId, @text)";

        foreach (string course in courses)
        {
            List<NpgsqlParameter> parameters = new List<NpgsqlParameter>(){
                new NpgsqlParameter("@recipeId", recipeId),
                new NpgsqlParameter("@text", course)
            };

            await DaoUtil.ExecuteAsync(_databaseConnectionSupplier.GetConnectionString(), sql, parameters);
        }
    }

    public Task DeleteAsync(int recipeId)
    {
        string sql = " DELETE FROM recipe_catalog.course" +
                     " WHERE recipe_id = @recipeId";

        return DaoUtil.ExecuteAsync(_databaseConnectionSupplier.GetConnectionString(), sql, new List<NpgsqlParameter>() { new NpgsqlParameter("@recipeId", recipeId) });
    }

    public Task<List<string>> GetAsync(int recipeId)
    {
        string sql = " SELECT text" +
                     " FROM recipe_catalog.course" +
                     " WHERE recipe_id = @recipeId";

        return DaoUtil.QueryForListAsync(_databaseConnectionSupplier.GetConnectionString(), sql, new CourseMapper(), new List<NpgsqlParameter>() { new NpgsqlParameter("@recipeId", recipeId) });
    }
}
