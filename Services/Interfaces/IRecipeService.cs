using recipe_catalog_api.Models;
using recipe_catalog_api.Models.Enums;

namespace recipe_catalog_api.Services.Interfaces;

public interface IRecipeService
{
    public Recipe? Get(int id);

    public List<Recipe> Get(List<string> courses, List<string> cuisines, List<string> tags, RecipeColumn? sortColumn, bool? reverse, string? name);

    public int Create(Recipe recipe);

    public void Update(Recipe recipe);

    public void Delete(int id);
}