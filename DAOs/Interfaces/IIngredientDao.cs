using food_history_api.Models;

namespace food_history_api.DAOs.Interfaces;

public interface IIngredientDao {
    public List<Ingredient> Get(int recipeId);

    public void Delete(int recipeId);

    public void Create(List<Ingredient> ingredientList, int recipeId);
}