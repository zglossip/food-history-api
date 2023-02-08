namespace food_history_api.Models;

public class Recipe {
    public int? Id {get; set;}

    public Uri? Link {get; set;}
    
    public string Name {get; set;}

    public List<string> CourseTypes {get; set;}

    public List<string> CuisineTypes {get; set;}

    public List<string> Tags { set; get;}

    public int ServingAmount {get; set;}

    public string ServingName {get; set;}

    public Uri? RecipeSourceUrl {get; set;}

    public Uri? Ingredients {get; set;}

    public Uri? Instructions {get; set;}

    public Recipe(int id, string name, List<String> courseTypes, List<String> cuisineTypes, List<string> tags, int servingAmount, string servingName, Uri? recipeSourceUrl) {
        this.Id = id;
        this.Name = name;
        this.CourseTypes = courseTypes;
        this.CuisineTypes = courseTypes;
        this.Tags = tags;
        this.ServingAmount = servingAmount;
        this.ServingName = servingName;
        this.RecipeSourceUrl = recipeSourceUrl;
    }
}