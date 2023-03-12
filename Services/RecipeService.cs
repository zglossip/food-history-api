using food_history_api.Models;
using food_history_api.DAOs.Interfaces;
using food_history_api.Services.Interfaces;

namespace food_history_api.Services;

public class RecipeService : IRecipeService{

    private readonly IRecipeDao _recipeDao;

    public RecipeService(IRecipeDao recipeDao) {
        _recipeDao = recipeDao;
    }

    public Recipe Get(int id) => _recipeDao.Get(id);

    public List<Recipe> GetAll() => _recipeDao.GetAll();

    public List<Recipe> GetForTags(List<string> tags) => _recipeDao.GetForTags(tags);

    public int Create(Recipe recipe) => _recipeDao.Create(recipe);

    public void Update(Recipe recipe) => _recipeDao.Update(recipe);

    public void Delete(int id) => _recipeDao.Delete(id);

}