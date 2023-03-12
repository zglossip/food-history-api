using food_history_api.Models;

namespace food_history_api.DAOs.Interfaces;

public interface IInstructionDao {
    public InstructionList Get(int recipeId);

    public void Update(InstructionList instructionList);
}