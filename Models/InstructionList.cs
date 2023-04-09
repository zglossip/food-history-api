namespace food_history_api.Models;

public class InstructionList {
    public int RecipeId {get; set;}

    public string? Recipe {get; set;}

    public List<string> Instructions {get; set;}

    //Only for use by deserialization
    public InstructionList()
    {
        this.RecipeId = -1;
        this.Instructions = new List<string>();
    }

    public InstructionList(int recipeId, List<string> instructions)
    {
        this.RecipeId = recipeId;
        this.Instructions = instructions;
    }
}