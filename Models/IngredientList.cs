namespace food_history_api.Models;

public class IngredientList {
    public int RecipeId {get; set;}

    public Uri? Recipe {get; set;}

    public List<Ingredient> Ingredients {get; set;}

    public IngredientList(int recipeId, List<Ingredient> ingredients) {
        this.RecipeId = recipeId;
        this.Ingredients = ingredients;
    }
}