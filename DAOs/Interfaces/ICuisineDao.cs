namespace food_history_api.DAOs.Interfaces;

public interface ICusineDao
{
    public List<string> Get(int recipeId);

    public void Delete(int recipeId);

    public void Create(List<string> cusisines, int recipeId);
}