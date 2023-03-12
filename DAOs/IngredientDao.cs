using food_history_api.Models;
using food_history_api.DAOs.Interfaces;
using System.Data.SqlClient;


namespace food_history_api.DAOs;

public class IngredientDao : IIngredientDao{
    public IngredientList Get(int recipeId) {
        string sql = "SELECT RECIPE_ID, NAME, QUANTITY, UOM, NOTES " +
                     "FROM zglossip.INGREDIENT " +
                     "WHERE RECIPE_ID = @recipeId";

        return null;
    }

    public void Update(IngredientList ingredientList){
        //TODO: Fill out
    }

}