using recipe_catalog_api.Models;

namespace recipe_catalog_api.DAOs.Interfaces;

public interface IIngredientDao
{
    public List<Ingredient> Get(int recipeId);

    public void Delete(int recipeId);

    public void Create(List<Ingredient> ingredientList, int recipeId);
}