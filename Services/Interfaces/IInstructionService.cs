using recipe_catalog_api.Models;

namespace recipe_catalog_api.Services.Interfaces;

public interface IInstructionService
{
    public InstructionList Get(int recipeId);

    public void Update(InstructionList instructionList);
}