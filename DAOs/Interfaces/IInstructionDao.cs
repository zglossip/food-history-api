using food_history_api.Models;

namespace food_history_api.DAOs.Interfaces;

public interface IInstructionDao {
    public List<string> Get(int recipeId);

    public void Delete(int recipeId);

    public void Create(List<string> instructionList, int recipeId);
}