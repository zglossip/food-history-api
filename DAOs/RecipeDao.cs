using food_history_api.Models;
using food_history_api.DAOs.Interfaces;
using food_history_api.DAOs.Mappers;
using food_history_api.DAOs.Util;

using Npgsql;
using food_history_api.Models.Enums;

namespace food_history_api.DAOs;

public class RecipeDao : IRecipeDao
{

    private readonly IDatabaseConnectionSupplier _databaseConnectionSupplier;

    public RecipeDao(IDatabaseConnectionSupplier databaseConnectionSupplier)
    {
        _databaseConnectionSupplier = databaseConnectionSupplier;
    }

    public Recipe? Get(int id)
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
        QueryParamList<string> queryParamList = new QueryParamList<string>("course", courses);

        string sql = " SELECT r.id, r.name, r.serving_amount, r.serving_name, r.source" +
                     " FROM food_history.recipe r" +
                     " JOIN food_history.course c" +
                     " ON r.id = c.recipe_id" +
                     " WHERE c.text in (" + queryParamList.GetQueryString() + ")" +
                     " GROUP BY r.id" +
                     " HAVING COUNT(DISTINCT c.text) = @coursesLength";

        List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();

        queryParamList.PopulateParamList(parameters);
        parameters.Add(new NpgsqlParameter("@coursesLength", courses.Count()));

        return DaoUtil.QueryForList(_databaseConnectionSupplier.GetConnectionString(), sql, new RecipeMapper(), parameters);
    }

    public List<Recipe> GetForCuisines(List<string> cuisines)
    {
        QueryParamList<string> queryParamList = new QueryParamList<string>("cusine", cuisines);

        string sql = " SELECT r.id, r.name, r.serving_amount, r.serving_name, r.source" +
                     " FROM food_history.recipe r" +
                     " JOIN food_history.cuisine c" +
                     " ON r.id = c.recipe_id" +
                     " WHERE c.text IN (" + queryParamList.GetQueryString() + ")" +
                     " GROUP BY r.id" +
                     " HAVING COUNT(DISTINCT c.text) = @cuisinesLength";

        List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();
        queryParamList.PopulateParamList(parameters);
        parameters.Add(new NpgsqlParameter("@cuisinesLength", cuisines.Count));

        return DaoUtil.QueryForList(_databaseConnectionSupplier.GetConnectionString(), sql, new RecipeMapper(), parameters);
    }

    
    public List<Recipe> GetForTags(List<string> tags)
    {
        QueryParamList<string> queryParamList = new QueryParamList<string>("tag", tags);

        string sql = " SELECT r.id, r.name, r.serving_amount, r.serving_name, r.source" +
                     " FROM food_history.recipe r" +
                     " JOIN food_history.tag t" +
                     " ON r.id = t.recipe_id" +
                     " WHERE t.text in (" + queryParamList.GetQueryString() + ")" +
                     " GROUP BY r.id" +
                     " HAVING COUNT(DISTINCT t.text) = @tagLength";

        List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();
        queryParamList.PopulateParamList(parameters);
        parameters.Add(new NpgsqlParameter("@tagLength", tags.Count()));

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
            new NpgsqlParameter("@source", recipe.RecipeSourceUrl == null ? DBNull.Value : recipe.RecipeSourceUrl.ToString())
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
            new NpgsqlParameter("@source", recipe.RecipeSourceUrl == null ? DBNull.Value : recipe.RecipeSourceUrl.ToString()),
            new NpgsqlParameter("@recipeId", recipe.Id)
        };

        DaoUtil.Execute(_databaseConnectionSupplier.GetConnectionString(), sql, parameters);
    }

    public void Delete(int id) {
        string sql = " DELETE FROM food_history.recipe" +
                     " WHERE id = @recipeId";

        DaoUtil.Execute(_databaseConnectionSupplier.GetConnectionString(), sql, new List<NpgsqlParameter>(){new NpgsqlParameter("@recipeId", id)});
    }

    List<Recipe> IRecipeDao.Get(List<string> courses, List<string> cuisines, List<string> tags, RecipeColumn? sortColumn)
    {
        QueryParamList<string> courseQueryParamList = new QueryParamList<string>("course", courses);
        QueryParamList<string> cuisineQueryParamList = new QueryParamList<string>("cuisine", cuisines);
        QueryParamList<string> tagQueryParamList = new QueryParamList<string>("tag", tags);

        string courseQuery = courseQueryParamList.GetQueryString();
        string cuisineQuery = cuisineQueryParamList.GetQueryString();
        string tagQuery = tagQueryParamList.GetQueryString();

        if (string.IsNullOrEmpty(courseQuery))
        {
            courseQuery = "''"; // replace empty list with default value
        }

        if (string.IsNullOrEmpty(cuisineQuery))
        {
            cuisineQuery = "''"; // replace empty list with default value
        }

        if (string.IsNullOrEmpty(tagQuery))
        {
            tagQuery = "''"; // replace empty list with default value
        }

        string sql = "SELECT r.* " +
                     "FROM food_history.recipe r " +
                     "WHERE (@courseLength = 0 OR (SELECT COUNT(*) AS courseCount " +
                     "        FROM food_history.course " +
                     "        WHERE text IN (" + courseQuery + ") " +
                     "          AND recipe_id = r.id) = @courseLength) " +
                     "  AND (@cuisineLength = 0 OR (SELECT COUNT(*) AS cuisineCount " +
                     "        FROM food_history.cuisine " +
                     "        WHERE text IN (" + cuisineQuery + ") " +
                     "          AND recipe_id = r.id) = @cuisineLength) " +
                     "  AND (@tagLength = 0 OR (SELECT COUNT(*) AS tagCount " +
                     "        FROM food_history.tag " +
                     "        WHERE text IN (" + tagQuery + ") " +
                     "          AND recipe_id = r.id) = @tagLength)";

        List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();
        courseQueryParamList.PopulateParamList(parameters);
        cuisineQueryParamList.PopulateParamList(parameters);
        tagQueryParamList.PopulateParamList(parameters);
        parameters.Add(new NpgsqlParameter("@courseLength", courses.Count));
        parameters.Add(new NpgsqlParameter("@cuisineLength", cuisines.Count));
        parameters.Add(new NpgsqlParameter("@tagLength", tags.Count));

        return DaoUtil.QueryForList(_databaseConnectionSupplier.GetConnectionString(), sql, new RecipeMapper(), parameters);
    }
}