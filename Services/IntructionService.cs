using food_history_api.Models;
using food_history_api.DAOs.Interfaces;
using food_history_api.Services.Interfaces;

namespace food_history_api.Services;

public class InstructionService : IInstructionService {

    private readonly IInstructionDao _instructionDao;

    public InstructionService(IInstructionDao instructionDao) {
        _instructionDao = instructionDao;
    }

    public InstructionList Get(int recipeId) => _instructionDao.Get(recipeId);

    public void Update(InstructionList instructionList) => _instructionDao.Update(instructionList);
}