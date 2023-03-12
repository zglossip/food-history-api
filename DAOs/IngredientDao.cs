using food_history_api.Models;
using food_history_api.DAOs.Interfaces;

using System.Data.SqlClient;
using Npgsql;

namespace food_history_api.DAOs;

public class IngredientDao : IIngredientDao
{

    private readonly IDatabaseConnectionSupplier _databaseConnectionSupplier;

    public IngredientDao(IDatabaseConnectionSupplier databaseConnectionSupplier)
    {
        _databaseConnectionSupplier = databaseConnectionSupplier;
    }
    
    public IngredientList Get(int recipeId)
    {
        List<Ingredient> ingredientList = new List<Ingredient>();

        string sql = "SELECT RECIPE_ID, INGREDIENT_NAME, QUANTITY, UOM, NOTES " +
                     "FROM food_history.INGREDIENT " +
                     "WHERE RECIPE_ID = @recipeId";

        DaoUtil.Query(DaoUtil.GetIngredientListAction(ingredientList), 
                      _databaseConnectionSupplier.GetConnectionString(), 
                      sql, 
                      new List<NpgsqlParameter>{new NpgsqlParameter("@recipeId", recipeId)});

        return new IngredientList(recipeId, ingredientList);
    }

    public void Delete(int recipeId)
    {
        string sql = "DELETE FROM food_history.INGREDIENT " +
                     "WHERE RECIPE_ID = @recipeId";

        DaoUtil.Execute(_databaseConnectionSupplier.GetConnectionString(), 
                        sql, 
                        new List<NpgsqlParameter>{new NpgsqlParameter("@recipeId", recipeId)});
    }

    public void Create(IngredientList ingredientList)
    {
        string sql = "INSERT INTO food_history.INGREDIENT (RECIPE_ID, INGREDIENT_NAME, QUANTITY, UOM, NOTES) " +
                     "VALUES(@recipeId, @name, @quantity, @uom, @notes)";


        ingredientList.Ingredients.ForEach(ingredient => 
        {
            List<NpgsqlParameter> sqlParameters = new List<NpgsqlParameter>
            {
                new NpgsqlParameter("@recipeId", ingredientList.RecipeId),
                new NpgsqlParameter("@name", ingredient.Name),
                new NpgsqlParameter("@quantity", ingredient.Quantity),
                new NpgsqlParameter("@uom", ingredient.Uom),
                new NpgsqlParameter("@notes", ingredient.Notes)
            };

            DaoUtil.Execute(_databaseConnectionSupplier.GetConnectionString(),
                            sql,
                            sqlParameters);
        });
    }

}