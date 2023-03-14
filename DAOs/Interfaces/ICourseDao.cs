namespace food_history_api.DAOs.Interfaces;

public interface ICourseDao
{
    public List<string> Get(int recipeId);

    public void Delete(int recipeId);

    public void Create(List<string> courses, int recipeId);
}