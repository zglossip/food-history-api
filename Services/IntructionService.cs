using food_history_api.Models;
using food_history_api.DAOs;

namespace food_history_api.Services;

public class InstructionService {
    public static InstructionList Get(int recipeId) => InstructionDao.Get(recipeId);

    public static void Update(InstructionList instructionList) => InstructionDao.Update(instructionList);
}