namespace food_history_api.Models;

public class Ingredient {
    public Uri Recipe {get; set;}
    public string Name {get; set;}

    public int Quantity {get; set;}

    public string Uom {get; set;}

    public string? Notes {get; set;}

    public Ingredient(Uri recipe, string name, int quantity, string uom, string notes) {
        this.Recipe = recipe;
        this.Name = name;
        this.Quantity = quantity;
        this.Uom = uom;
        this.Notes = notes;
    }

}