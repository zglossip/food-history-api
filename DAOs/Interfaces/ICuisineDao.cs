namespace recipe_catalog_api.DAOs.Interfaces;

public interface ICuisineDao
{
    public List<string> Get(int recipeId);

    public void Delete(int recipeId);

    public void Create(List<string> cusisines, int recipeId);
}