namespace food_history_api.DAOs.Interfaces;

public interface ITagDao
{
    public List<string> Get(int recipeId);

    public void Delete(int recipeId);

    public void Create(List<string> tags, int recipeId);
}