using recipe_catalog_api.Models;
using recipe_catalog_api.DAOs.Interfaces;
using recipe_catalog_api.Services.Interfaces;

namespace recipe_catalog_api.Services;

public class InstructionService(IInstructionDao instructionDao) : IInstructionService
{
    private readonly IInstructionDao _instructionDao = instructionDao;

    public async Task<InstructionList> GetAsync(int recipeId) =>
        new InstructionList(recipeId, await _instructionDao.GetAsync(recipeId));

    public async Task UpdateAsync(InstructionList instructionList)
    {
        await _instructionDao.DeleteAsync(instructionList.RecipeId);
        await _instructionDao.CreateAsync(instructionList.Instructions, instructionList.RecipeId);
    }
}
