using food_history_api.Models;
using food_history_api.Services.Interfaces;

namespace food_history_api.Services;

public class ParserService(IRecipeService recipeService, IIngredientService ingredientService, IInstructionService instructionService) : IParserService
{
    private readonly IRecipeService _recipeService = recipeService;
    private readonly IIngredientService _ingredientService = ingredientService;
    private readonly IInstructionService _instructionService = instructionService;

    public int Parse(string input)
    {
        //TODO Finish this 

        var lines = input.Split('\n');

        Recipe recipe = new();

        return -1;
    }
}