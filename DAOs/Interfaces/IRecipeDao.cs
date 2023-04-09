using food_history_api.Models;

namespace food_history_api.DAOs.Interfaces;

public interface IRecipeDao {
    public Recipe? Get(int id);

    public List<Recipe> GetAll();

    public List<Recipe> GetForCourses(List<string> courses);

    public List<Recipe> GetForCuisines(List<string> cuisines);

    public List<Recipe> GetForTags(List<string> tags);

    public int Create(Recipe recipe);

    public void Update(Recipe recipe);

    public void Delete(int id);
}