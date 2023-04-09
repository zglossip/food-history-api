using food_history_api.Models;
using food_history_api.Models.Enums;

namespace food_history_api.DAOs.Interfaces;

public interface IRecipeDao {
    public Recipe? Get(int id);

    public List<Recipe> Get(List<string> courses, List<string> cuisines, List<string> tags, RecipeColumn? sortColumn);

    public int Create(Recipe recipe);

    public void Update(Recipe recipe);

    public void Delete(int id);
}