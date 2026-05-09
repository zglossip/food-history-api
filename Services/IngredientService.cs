using recipe_catalog_api.Models;
using recipe_catalog_api.DAOs.Interfaces;
using recipe_catalog_api.Services.Interfaces;

namespace recipe_catalog_api.Services;

public class IngredientService(IIngredientDao ingredientDao) : IIngredientService
{
    private readonly IIngredientDao _ingredientDao = ingredientDao;

    public IngredientList Get(int recipeId) => new IngredientList(recipeId, _ingredientDao.Get(recipeId));

    public void Update(IngredientList ingredientList)
    {
        _ingredientDao.Delete(ingredientList.RecipeId);
        _ingredientDao.Create(ingredientList.Ingredients, ingredientList.RecipeId);
    }
}