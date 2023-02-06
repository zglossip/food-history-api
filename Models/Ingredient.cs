namespace food_history_api.Models;

public class Ingredient {
    public int? Id {get; set;}
    public Uri Recipe {get; set;}
    public string Name {get; set;}

    public int Amount {get; set;}

    public string UnitName {get; set;}

    public string? Notes {get; set;}

    public Ingredient(int id, Uri recipe, string name, int amount, string unitName, string notes) {
        this.Id = id;
        this.Recipe = recipe;
        this.Name = name;
        this.Amount = amount;
        this.UnitName = unitName;
        this.Notes = notes;
    }

}