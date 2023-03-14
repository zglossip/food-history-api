using food_history_api.Models;
using food_history_api.DAOs.Interfaces;
using food_history_api.DAOs.Mappers;

using Npgsql;

namespace food_history_api.DAOs;

public class RecipeDao : IRecipeDao
{

    private readonly IDatabaseConnectionSupplier _databaseConnectionSupplier;

    public RecipeDao(IDatabaseConnectionSupplier databaseConnectionSupplier)
    {
        _databaseConnectionSupplier = databaseConnectionSupplier;
    }

    public Recipe Get(int id)
    {
        string sql = " SELECT id, name, serving_amount, serving_name, source" +
                     " FROM food_history.recipe" +
                     " WHERE id = @recipeId";

        return DaoUtil.Query(_databaseConnectionSupplier.GetConnectionString(), sql, new RecipeMapper(), new List<NpgsqlParameter>(){new NpgsqlParameter("@recipeId", id)});
    }

    public List<Recipe> GetAll() {
        string sql = " SELECT id, name, serving_amount, serving_name, source" +
                     " FROM food_history.recipe";

        return DaoUtil.QueryForList(_databaseConnectionSupplier.GetConnectionString(), sql, new RecipeMapper());
    }

    public List<Recipe> GetForCourses(List<string> courses)
    {
        string sql = " SELECT r.id, r.name, r.serving_amount, r.serving_name, r.source" +
                     " FROM food_history.recipe r" +
                     " JOIN food_history.course c" +
                     " ON r.id = c.recipe_id" +
                     " WHERE c.text in (@courses)" +
                     " GROUP BY r.id" +
                     " HAVING COUNT(DISTINCT c.text) = @coursesLength";

        List<NpgsqlParameter> parameters = new List<NpgsqlParameter>()
        {
            new NpgsqlParameter("@courses", courses),
            new NpgsqlParameter("@coursesLength", courses.Count())
        };

        return DaoUtil.QueryForList(_databaseConnectionSupplier.GetConnectionString(), sql, new RecipeMapper(), parameters);
    }

    public List<Recipe> GetForCuisines(List<string> cuisines)
    {
        string sql = " SELECT r.id, r.name, r.serving_amount, r.serving_name, r.source" +
                     " FROM food_history.recipe r" +
                     " JOIN food_history.cuisine c" +
                     " ON r.id = c.recipe_id" +
                     " WHERE c.text in (@cuisines)" +
                     " GROUP BY r.id" +
                     " HAVING COUNT(DISTINCT c.text) = @cuisinesLength";

        List<NpgsqlParameter> parameters = new List<NpgsqlParameter>()
        {
            new NpgsqlParameter("@cuisines", cuisines),
            new NpgsqlParameter("@cuisinesLength", cuisines.Count())
        };

        return DaoUtil.QueryForList(_databaseConnectionSupplier.GetConnectionString(), sql, new RecipeMapper(), parameters);
    }
    
    public List<Recipe> GetForTags(List<string> tags) {
        string sql = " SELECT r.id, r.name, r.serving_amount, r.serving_name, r.source" +
                     " FROM food_history.recipe r" +
                     " JOIN food_history.tag t" +
                     " ON r.id = t.recipe_id" +
                     " WHERE t.text in (@tags)" +
                     " GROUP BY r.id" +
                     " HAVING COUNT(DISTINCT t.text) = @tagLength";

        List<NpgsqlParameter> parameters = new List<NpgsqlParameter>()
        {
            new NpgsqlParameter("@tags", tags),
            new NpgsqlParameter("@tagLength", tags.Count())
        };

        return DaoUtil.QueryForList(_databaseConnectionSupplier.GetConnectionString(), sql, new RecipeMapper(), parameters);
    }

    public int Create(Recipe recipe) {
        string sql = " INSERT INTO food_history.recipe" +
                     " (name, serving_amount, serving_name, source)" +
                     " VALUES" +
                     " (@name, @servingAmount, @servingName, @source)" +
                     " RETURNING id";

        List<NpgsqlParameter> parameters = new List<NpgsqlParameter>()
        {
            new NpgsqlParameter("@name", recipe.Name),
            new NpgsqlParameter("@servingAmount", recipe.ServingAmount),
            new NpgsqlParameter("@servingName", recipe.ServingName),
            new NpgsqlParameter("@source", recipe.RecipeSourceUrl.ToString())
        };

        return DaoUtil.Create(_databaseConnectionSupplier.GetConnectionString(), sql, parameters);
    }

    public void Update(Recipe recipe) {
        string sql = " UPDATE food_history.recipe" +
                     " SET name = @name, serving_amount = @servingAmount, serving_name = @servingName, source = @source" +
                     " WHERE id = @recipeId";

        List<NpgsqlParameter> parameters = new List<NpgsqlParameter>()
        {
            new NpgsqlParameter("@name", recipe.Name),
            new NpgsqlParameter("@servingAmount", recipe.ServingAmount),
            new NpgsqlParameter("@servingName", recipe.ServingName),
            new NpgsqlParameter("@source", recipe.RecipeSourceUrl),
            new NpgsqlParameter("@recipeId", recipe.Id)
        };

        DaoUtil.Execute(_databaseConnectionSupplier.GetConnectionString(), sql, parameters);
    }

    public void Delete(int id) {
        string sql = " DELETE FROM food_history.recipe" +
                     " WHERE id = @recipeId";

        DaoUtil.Execute(_databaseConnectionSupplier.GetConnectionString(), sql, new List<NpgsqlParameter>(){new NpgsqlParameter("@recipeId", id)});
    }
}