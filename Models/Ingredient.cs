namespace food_history_api.Models;

public class Ingredient {
    public Uri? Recipe {get; set;}
    public int RecipeId {get; set;}
    public string Name {get; set;}

    public int Quantity {get; set;}

    public string Uom {get; set;}

    public string? Notes {get; set;}

    public Ingredient(int recipeId, string name, int quantity, string uom, string notes) {
        this.RecipeId = recipeId;
        this.Name = name;
        this.Quantity = quantity;
        this.Uom = uom;
        this.Notes = notes;
    }

}