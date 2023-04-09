namespace food_history_api.Models;

public class IngredientList {
    public int RecipeId {get; set;}

    public string? Recipe {get; set;}

    public List<Ingredient> Ingredients {get; set;}

    //Only for use by deserialization
    public IngredientList()
    {
        this.RecipeId = -1;
        this.Ingredients = new List<Ingredient>();
    }

    public IngredientList(int recipeId, List<Ingredient> ingredients) {
        this.RecipeId = recipeId;
        this.Ingredients = ingredients;
    }
}