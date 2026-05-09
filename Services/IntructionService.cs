using food_history_api.Models;
using food_history_api.DAOs.Interfaces;
using food_history_api.Services.Interfaces;

namespace food_history_api.Services;

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