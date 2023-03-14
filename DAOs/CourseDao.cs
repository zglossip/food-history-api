using food_history_api.DAOs.Interfaces;
using food_history_api.DAOs.Mappers;

using Npgsql;

namespace food_history_api.DAOs;

public class CourseDao : ICourseDao
{

    private readonly IDatabaseConnectionSupplier _databaseConnectionSupplier;

    public CourseDao(IDatabaseConnectionSupplier databaseConnectionSupplier)
    {
        _databaseConnectionSupplier = databaseConnectionSupplier;
    }

    public void Create(List<string> courses, int recipeId)
    {
        string sql = " INSERT INTO food_history.course" +
                     " (recipe_id, text)" +
                     " VALUES (@recipeId, @text)";

        courses.ForEach(course => {
            List<NpgsqlParameter> parameters = new List<NpgsqlParameter>(){
                new NpgsqlParameter("@recipeId", recipeId),
                new NpgsqlParameter("@text", course)
            };

            DaoUtil.Create(_databaseConnectionSupplier.GetConnectionString(), sql, parameters);
        });
    }

    public void Delete(int recipeId)
    {
        string sql = " DELETE FROM food_history.course" +
                     " WHERE recipe_id = @recipeId";

        DaoUtil.Execute(_databaseConnectionSupplier.GetConnectionString(), sql, new List<NpgsqlParameter>(){new NpgsqlParameter("@recipeId", recipeId)});
    }

    public List<string> Get(int recipeId)
    {
        string sql = " SELECT text" +
                     " FROM food_history.course" +
                     " WHERE recipe_id = @recipeId";

        return DaoUtil.QueryForList(_databaseConnectionSupplier.GetConnectionString(), sql, new CourseMapper(), new List<NpgsqlParameter>(){new NpgsqlParameter("@recipeId", recipeId)});
    }
}