using food_history_api.Models;
using food_history_api.DAOs.Interfaces;

using System.Data.SqlClient;

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

        string sql = "SELECT RECIPE_ID, NAME, QUANTITY, UOM, NOTES " +
                     "FROM zglossip.INGREDIENT " +
                     "WHERE RECIPE_ID = @recipeId";

        DaoUtil.Query(DaoUtil.GetIngredientListAction(ingredientList), 
                      _databaseConnectionSupplier.GetConnectionString(), 
                      sql, 
                      new List<SqlParameter>{new SqlParameter("@recipeId", recipeId)});

        return new IngredientList(recipeId, ingredientList);
    }

    public void Delete(int recipeId)
    {
        string sql = "DELETE FROM zglossip.INGREDIENT " +
                     "WHERE RECIPE_ID = @recipeId";

        DaoUtil.Execute(_databaseConnectionSupplier.GetConnectionString(), 
                        sql, 
                        new List<SqlParameter>{new SqlParameter("@recipeId", recipeId)});
    }

    public void Create(IngredientList ingredientList)
    {
        string sql = "INSERT INTO zglossip.INGREDIENT (RECIPE_ID, NAME, QUANTITY, UOM, NOTES) " +
                     "VALUES(@recipeId, @name, @quantity, @uom, @notes)";


        ingredientList.Ingredients.ForEach(ingredient => 
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@recipeId", ingredientList.RecipeId),
                new SqlParameter("@name", ingredient.Name),
                new SqlParameter("@quantity", ingredient.Quantity),
                new SqlParameter("@uom", ingredient.Uom),
                new SqlParameter("@notes", ingredient.Notes)
            };

            DaoUtil.Execute(_databaseConnectionSupplier.GetConnectionString(),
                            sql,
                            sqlParameters);
        });
    }

}