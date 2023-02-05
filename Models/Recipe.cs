namespace food_history_api.Models;

public class Recipe {
    public int? Id {get; set;}
    public string Name {get; set;}

    public List<string> CourseTypes {get; set;}

    public List<string> CuisineTypes {get; set;}

    public int ServingAmount {get; set;}

    public string ServingName {get; set;}

    public string? RecipeSourceUrl {get; set;}

    public List<Ingredient>? Ingredients {get; set;}

    public List<string>? Instructions {get; set;}

    public Recipe(int id, string name, List<String> courseTypes, List<String> cuisineTypes, int servingAmount, string servingName, string? recipeSourceUrl) {
        this.Id = id;
        this.Name = name;
        this.CourseTypes = courseTypes;
        this.CuisineTypes = courseTypes;
        this.ServingAmount = servingAmount;
        this.ServingName = servingName;
        this.RecipeSourceUrl = recipeSourceUrl;
    }
}