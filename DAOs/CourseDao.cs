using recipe_catalog_api.DAOs.Interfaces;
using recipe_catalog_api.DAOs.Mappers;
using recipe_catalog_api.DAOs.Util;

using Npgsql;

namespace recipe_catalog_api.DAOs;

public class CourseDao(IDatabaseConnectionSupplier databaseConnectionSupplier) : ICourseDao
{

    private readonly IDatabaseConnectionSupplier _databaseConnectionSupplier = databaseConnectionSupplier;

    public void Create(List<string> courses, int recipeId)
    {
        string sql = " INSERT INTO recipe_catalog.course" +
                     " (recipe_id, text)" +
                     " VALUES (@recipeId, @text)";

        courses.ForEach(course =>
        {
            List<NpgsqlParameter> parameters = new List<NpgsqlParameter>(){
                new NpgsqlParameter("@recipeId", recipeId),
                new NpgsqlParameter("@text", course)
            };

            DaoUtil.Create(_databaseConnectionSupplier.GetConnectionString(), sql, parameters);
        });
    }

    public void Delete(int recipeId)
    {
        string sql = " DELETE FROM recipe_catalog.course" +
                     " WHERE recipe_id = @recipeId";

        DaoUtil.Execute(_databaseConnectionSupplier.GetConnectionString(), sql, new List<NpgsqlParameter>() { new NpgsqlParameter("@recipeId", recipeId) });
    }

    public List<string> Get(int recipeId)
    {
        string sql = " SELECT text" +
                     " FROM recipe_catalog.course" +
                     " WHERE recipe_id = @recipeId";

        return DaoUtil.QueryForList(_databaseConnectionSupplier.GetConnectionString(), sql, new CourseMapper(), new List<NpgsqlParameter>() { new NpgsqlParameter("@recipeId", recipeId) });
    }
}