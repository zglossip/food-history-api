namespace food_history_api.Models;

public class Ingredient {
    public string Name {get; set;}

    public int Quantity {get; set;}

    public string Uom {get; set;}

    public string? Notes {get; set;}

    //Only for use by deserialization
    public Ingredient()
    {
        this.Name = "ERROR";
        this.Quantity = -1;
        this.Uom = "ERROR";
    }

    public Ingredient(string name, int quantity, string uom, string? notes)
    {
        this.Name = name;
        this.Quantity = quantity;
        this.Uom = uom;
        this.Notes = notes;
    }

}