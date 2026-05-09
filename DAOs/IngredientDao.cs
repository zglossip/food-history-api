using recipe_catalog_api.Models;
using recipe_catalog_api.DAOs.Interfaces;
using recipe_catalog_api.DAOs.Mappers;
using recipe_catalog_api.DAOs.Util;

using Npgsql;

namespace recipe_catalog_api.DAOs;

public class IngredientDao(IDatabaseConnectionSupplier databaseConnectionSupplier) : IIngredientDao
{

    private readonly IDatabaseConnectionSupplier _databaseConnectionSupplier = databaseConnectionSupplier;

    public List<Ingredient> Get(int recipeId)
    {
        string sql = "SELECT NAME, QUANTITY, UOM, NOTES " +
                     "FROM recipe_catalog.INGREDIENT " +
                     "WHERE RECIPE_ID = @recipeId " +
                     "ORDER BY POSITION ASC";

        return DaoUtil.QueryForList(_databaseConnectionSupplier.GetConnectionString(),
                      sql,
                      new IngredientMapper(),
                      new List<NpgsqlParameter> { new NpgsqlParameter("@recipeId", recipeId) });
    }

    public void Delete(int recipeId)
    {
        string sql = "DELETE FROM recipe_catalog.INGREDIENT " +
                     "WHERE RECIPE_ID = @recipeId";

        DaoUtil.Execute(_databaseConnectionSupplier.GetConnectionString(),
                        sql,
                        new List<NpgsqlParameter> { new NpgsqlParameter("@recipeId", recipeId) });
    }

    public void Create(List<Ingredient> ingredientList, int recipeId)
    {
        string sql = "INSERT INTO recipe_catalog.INGREDIENT (RECIPE_ID, POSITION, NAME, QUANTITY, UOM, NOTES) " +
                     "VALUES(@recipeId, @position, @name, @quantity, @uom, @notes)";

        int position = 0;

        ingredientList.ForEach(ingredient =>
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

            DaoUtil.Create(_databaseConnectionSupplier.GetConnectionString(),
                            sql,
                            sqlParameters);
        });
    }

}