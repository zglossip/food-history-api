using recipe_catalog_api.Models;

namespace recipe_catalog_api.Services.Interfaces;

public interface IInstructionService
{
    public Task<InstructionList> GetAsync(int recipeId);

    public Task UpdateAsync(InstructionList instructionList);
}
