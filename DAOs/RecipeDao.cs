using food_history_api.Models;
using food_history_api.DAOs.Interfaces;

namespace food_history_api.DAOs;

public class RecipeDao : IRecipeDao {
    public Recipe Get(int id) {
        //TODO: Fill out
        return null;
    }

    public List<Recipe> GetAll() {
        //TODO: Fill out
        return null;
    }

    public List<Recipe> GetForTags(List<string> tags) {
        //TODO: Fill out
        return null;
    }

    public int Create(Recipe recipe) {
        //TODO: Fill out
        return -1;
    }

    public void Update(Recipe recipe) {
        //TODO: Fill out
    }

    public void Delete(int id) {
        //TODO: Fill out
    }
}