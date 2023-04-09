using food_history_api.Models;
using food_history_api.Models.Enums;

namespace food_history_api.Services.Interfaces;

public interface IRecipeService {
    public Recipe? Get(int id);

    public List<Recipe> GetAll();

    public List<Recipe> GetForCourses(List<string> courses);

    public List<Recipe> GetForCuisines(List<string> cuisines);

    public List<Recipe> GetForTags(List<string> tags);

    public List<Recipe> Get(List<string> courses, List<string> cuisines, List<string> tags, RecipeColumn? sortColumn, bool? reverse);

    public int Create(Recipe recipe);

    public void Update(Recipe recipe);

    public void Delete(int id);
}