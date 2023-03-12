using food_history_api.Models;

namespace food_history_api.DAOs.Interfaces;

public interface IIngredientDao {
    public IngredientList Get(int recipeId);

    public void Update(IngredientList ingredientList);
}