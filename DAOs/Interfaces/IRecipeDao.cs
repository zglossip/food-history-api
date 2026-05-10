using recipe_catalog_api.Models;
using recipe_catalog_api.Models.Enums;

namespace recipe_catalog_api.DAOs.Interfaces;

public interface IRecipeDao
{
    public Recipe? Get(int id);

    public List<Recipe> Get(List<string> courses, List<string> cuisines, List<string> tags, RecipeColumn? sortColumn, string? name);

    public int Create(RecipeRequest recipe);

    public void Update(int id, RecipeRequest recipe);

    public void Delete(int id);
}