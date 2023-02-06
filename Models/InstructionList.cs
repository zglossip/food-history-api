namespace food_history_api.Models;

public class InstructionList {
    public int RecipeId {get; set;}

    public Uri? Recipe {get; set;}

    public string Instruction {get; set;}

    public InstructionList(int recipeId, string instruction){
        this.RecipeId = recipeId;
        this.Instruction = instruction;
    }
}