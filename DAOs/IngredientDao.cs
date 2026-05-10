using recipe_catalog_api.Models;
using recipe_catalog_api.DAOs.Interfaces;
using recipe_catalog_api.DAOs.Mappers;
using recipe_catalog_api.DAOs.Util;

using Npgsql;

namespace recipe_catalog_api.DAOs;

public class IngredientDao(IDatabaseConnectionSupplier databaseConnectionSupplier) : IIngredientDao
{

    private readonly IDatabaseConnectionSupplier _databaseConnectionSupplier = databaseConnectionSupplier;

    public Task<List<Ingredient>> GetAsync(int recipeId)
    {
        string sql = "SELECT NAME, QUANTITY, UOM, NOTES " +
                     "FROM recipe_catalog.INGREDIENT " +
                     "WHERE RECIPE_ID = @recipeId " +
                     "ORDER BY POSITION ASC";

        return DaoUtil.QueryForListAsync(_databaseConnectionSupplier.GetConnectionString(),
                      sql,
                      new IngredientMapper(),
                      new List<NpgsqlParameter> { new NpgsqlParameter("@recipeId", recipeId) });
    }

    public Task DeleteAsync(int recipeId)
    {
        string sql = "DELETE FROM recipe_catalog.INGREDIENT " +
                     "WHERE RECIPE_ID = @recipeId";

        return DaoUtil.ExecuteAsync(_databaseConnectionSupplier.GetConnectionString(),
                        sql,
                        new List<NpgsqlParameter> { new NpgsqlParameter("@recipeId", recipeId) });
    }

    public async Task CreateAsync(List<Ingredient> ingredientList, int recipeId)
    {
        string sql = "INSERT INTO recipe_catalog.INGREDIENT (RECIPE_ID, POSITION, NAME, QUANTITY, UOM, NOTES) " +
                     "VALUES(@recipeId, @position, @name, @quantity, @uom, @notes)";

        int position = 0;

        foreach (Ingredient ingredient in ingredientList)
        {
            List<NpgsqlParameter> sqlParameters = new List<NpgsqlParameter>
            {
                new NpgsqlParameter("@recipeId", recipeId),
                new NpgsqlParameter("@position", position++),
                new NpgsqlParameter("@name", ingredient.Name),
                new NpgsqlParameter("@quantity", ingredient.Quantity),
                new NpgsqlParameter("@uom", ingredient.Uom == null ? DBNull.Value : ingredient.Uom),
                new NpgsqlParameter("@notes", ingredient.Notes == null ? DBNull.Value : ingredient.Notes)
            };

            await DaoUtil.ExecuteAsync(_databaseConnectionSupplier.GetConnectionString(),
                            sql,
                            sqlParameters);
        }
    }

}
