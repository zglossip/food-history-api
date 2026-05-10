using recipe_catalog_api.Models;

namespace recipe_catalog_api.DAOs.Interfaces;

public interface IIngredientDao
{
    public Task<List<Ingredient>> GetAsync(int recipeId);

    public Task DeleteAsync(int recipeId);

    public Task CreateAsync(List<Ingredient> ingredientList, int recipeId);
}
