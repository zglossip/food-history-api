using food_history_api.Models;
using food_history_api.DAOs;

namespace food_history_api.Services;

public class RecipeService {

    public static Recipe Get(int id) => RecipeDao.Get(id);

    public static List<Recipe> GetAll() => RecipeDao.GetAll();

    public static List<Recipe> GetForTags(List<string> tags) => RecipeDao.GetForTags(tags);

    public static int Create(Recipe recipe) => RecipeDao.Create(recipe);

    public static void Update(Recipe recipe) => RecipeDao.Update(recipe);

    public static void Delete(int id) => RecipeDao.Delete(id);

}