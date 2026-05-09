using recipe_catalog_api.Models;

namespace recipe_catalog_api.DAOs.Interfaces;

public interface IInstructionDao
{
    public List<string> Get(int recipeId);

    public void Delete(int recipeId);

    public void Create(List<string> instructionList, int recipeId);
}