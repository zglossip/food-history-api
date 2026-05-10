using recipe_catalog_api.Models;
using recipe_catalog_api.DAOs.Interfaces;
using recipe_catalog_api.Services.Interfaces;

namespace recipe_catalog_api.Services;

public class IngredientService(IIngredientDao ingredientDao) : IIngredientService
{
    private readonly IIngredientDao _ingredientDao = ingredientDao;

    public async Task<IngredientList> GetAsync(int recipeId) =>
        new IngredientList(recipeId, await _ingredientDao.GetAsync(recipeId));

    public async Task UpdateAsync(IngredientList ingredientList)
    {
        await _ingredientDao.DeleteAsync(ingredientList.RecipeId);
        await _ingredientDao.CreateAsync(ingredientList.Ingredients, ingredientList.RecipeId);
    }
}
