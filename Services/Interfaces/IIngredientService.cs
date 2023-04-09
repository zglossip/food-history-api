namespace food_history_api.Services.Interfaces;

using food_history_api.Models;

public interface IIngredientService {
    public IngredientList Get(int recipeId);

    public void Update(IngredientList ingredientList);
}