using food_history_api.Models;
using food_history_api.DAOs.Interfaces;
using food_history_api.Services.Interfaces;

namespace food_history_api.Services;

public class IngredientService : IIngredientService {

    private readonly IIngredientDao _ingredientDao;

    public IngredientService(IIngredientDao ingredientDao) {
        _ingredientDao = ingredientDao;
    }

    public IngredientList Get(int recipeId) => _ingredientDao.Get(recipeId);

    public void Update(IngredientList ingredientList) => _ingredientDao.Update(ingredientList);
}