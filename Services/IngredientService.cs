using food_history_api.Models;
using food_history_api.DAOs.Interfaces;
using food_history_api.Services.Interfaces;

namespace food_history_api.Services;

public class IngredientService : IIngredientService {

    private readonly IIngredientDao _ingredientDao;

    public IngredientService(IIngredientDao ingredientDao) {
        _ingredientDao = ingredientDao;
    }

    public IngredientList Get(int recipeId) => new IngredientList(recipeId, _ingredientDao.Get(recipeId));

    public void Upsert(IngredientList ingredientList) {
        _ingredientDao.Delete(ingredientList.RecipeId);
        _ingredientDao.Create(ingredientList.Ingredients, ingredientList.RecipeId);
    }
}