namespace food_history_api.Models;

public class InstructionList {
    public int RecipeId {get; set;}

    public Uri? Recipe {get; set;}

    public List<string> Instructions {get; set;}

    public InstructionList(int recipeId, List<string> instructions){
        this.RecipeId = recipeId;
        this.Instructions = instructions;
    }
}