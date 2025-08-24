using food_history_api.Models;
using food_history_api.Services.Interfaces;

namespace food_history_api.Services;

public class ParserService : IParserService
{
    private readonly IRecipeService _recipeService;
    private readonly IIngredientService _ingredientService;
    private readonly IInstructionService _instructionService;

    public ParserService(IRecipeService recipeService, IIngredientService ingredientService, IInstructionService instructionService)
    {
        _recipeService = recipeService;
        _ingredientService = ingredientService;
        _instructionService = instructionService;
    }

    public int Parse(string input)
    {
        //TODO Finish this 

        var lines = input.Split('\n');

        Recipe recipe = new();

        return -1;
    }
}