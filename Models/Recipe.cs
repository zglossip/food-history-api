using Newtonsoft.Json;

namespace food_history_api.Models;

public class Recipe {
    [JsonIgnore]
    public int? Id {get; set;}

    [JsonIgnore]
    public Uri? Link {get; set;}
    
    public string Name {get; set;}

    public List<string> CourseTypes {get; set;}

    public List<string> CuisineTypes {get; set;}

    public List<string> Tags { set; get;}

    public int ServingAmount {get; set;}

    public string ServingName {get; set;}

    [JsonIgnore]
    public Uri? RecipeSourceUrl {get; set;}

    [JsonIgnore]
    public Uri? Ingredients {get; set;}

    [JsonIgnore]
    public Uri? Instructions {get; set;}

    public Recipe(int id, string name, int servingAmount, string servingName, Uri? recipeSourceUrl) {
        this.Id = id;
        this.Name = name;
        this.CourseTypes = new List<string>();
        this.CuisineTypes = new List<string>();
        this.Tags = new List<String>();
        this.ServingAmount = servingAmount;
        this.ServingName = servingName;
        this.RecipeSourceUrl = recipeSourceUrl;
    }
}