using food_history_api.Models;

namespace food_history_api.Services.Interfaces;

public interface IInstructionService {
    public InstructionList Get(int recipeId);

    public void Update(InstructionList instructionList);
}