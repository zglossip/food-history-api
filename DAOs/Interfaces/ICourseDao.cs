namespace recipe_catalog_api.DAOs.Interfaces;

public interface ICourseDao
{
    public Task<List<string>> GetAsync(int recipeId);

    public Task DeleteAsync(int recipeId);

    public Task CreateAsync(List<string> courses, int recipeId);
}
