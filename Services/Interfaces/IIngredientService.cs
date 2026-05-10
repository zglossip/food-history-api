namespace recipe_catalog_api.Services.Interfaces;

using recipe_catalog_api.Models;

public interface IIngredientService
{
    public Task<IngredientList> GetAsync(int recipeId);

    public Task UpdateAsync(IngredientList ingredientList);
}
