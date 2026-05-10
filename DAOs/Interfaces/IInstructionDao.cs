using recipe_catalog_api.Models;

namespace recipe_catalog_api.DAOs.Interfaces;

public interface IInstructionDao
{
    public Task<List<string>> GetAsync(int recipeId);

    public Task DeleteAsync(int recipeId);

    public Task CreateAsync(List<string> instructionList, int recipeId);
}
