using food_history_api.Models;
using food_history_api.DAOs;

namespace food_history_api.Services;

public class IngredientService {
    public static IngredientList Get(int recipeId) => IngredientDao.Get(recipeId);

    public static void Update(IngredientList ingredientList) => IngredientDao.Update(ingredientList);
}