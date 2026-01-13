namespace food_history_api.Models;

public class Recipe {
    public int? Id {get; set;}
    
    public string Name {get; set;}

    public List<string> CourseTypes {get; set;}

    public List<string> CuisineTypes {get; set;}

    public List<string> Tags { set; get;}

    public int ServingAmount {get; set;}

    public string ServingName {get; set;}

    public string? Source {get; set;}

    public DateTime? Uploaded { get; set; }

    //Only for use by deserialization
    public Recipe()
    {
        this.Name = "ERROR";
        this.CourseTypes = new List<string>();
        this.CuisineTypes = new List<string>();
        this.Tags = new List<string>();
        this.ServingAmount = -1;
        this.ServingName = "ERROR";
    }

    public Recipe(int Id, string Name, int ServingAmount, string ServingName, string? Source, DateTime Uploaded)
    {
        this.Id = Id;
        this.Name = Name;
        this.CourseTypes = [];
        this.CuisineTypes = [];
        this.Tags = [];
        this.ServingAmount = ServingAmount;
        this.ServingName = ServingName;
        this.Source = Source;
        this.Uploaded = Uploaded;
    }

    public Recipe(Recipe recipe)
    {
        Id = recipe.Id;
        Name = recipe.Name;
        CourseTypes = recipe.CourseTypes;
        CuisineTypes = recipe.CuisineTypes;
        Tags = recipe.Tags;
        ServingAmount = recipe.ServingAmount;
        ServingName = recipe.ServingName;
        Source = recipe.Source;
        Uploaded = recipe.Uploaded;
    }

    public Recipe Clone()
    {
        return new Recipe(this);
    }
}
