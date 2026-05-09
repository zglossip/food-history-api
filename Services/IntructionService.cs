using recipe_catalog_api.Models;
using recipe_catalog_api.DAOs.Interfaces;
using recipe_catalog_api.Services.Interfaces;

namespace recipe_catalog_api.Services;

public class InstructionService(IInstructionDao instructionDao) : IInstructionService
{
    private readonly IInstructionDao _instructionDao = instructionDao;

    public InstructionList Get(int recipeId) => new InstructionList(recipeId, _instructionDao.Get(recipeId));

    public void Update(InstructionList instructionList)
    {
        _instructionDao.Delete(instructionList.RecipeId);
        _instructionDao.Create(instructionList.Instructions, instructionList.RecipeId);
    }
}