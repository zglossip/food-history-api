using food_history_api.Models;
using System.Data.SqlClient;


namespace food_history_api.DAOs;

public class IngredientDao {
    public static IngredientList Get(int recipeId) {
        string sql = "SELECT RECIPE_ID, NAME, QUANTITY, UOM, NOTES " +
                     "FROM zglossip.INGREDIANT " +
                     "WHERE RECIPE_ID = @recipeId";

        return null;
    }

    public static void Update(IngredientList ingredientList){
        //TODO: Fill out
    }

}