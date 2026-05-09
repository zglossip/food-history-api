namespace recipe_catalog_api.Services.Interfaces;

using recipe_catalog_api.Models;

public interface IIngredientService
{
    public IngredientList Get(int recipeId);

    public void Update(IngredientList ingredientList);
}