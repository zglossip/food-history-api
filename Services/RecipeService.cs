using food_history_api.Models;
using food_history_api.DAOs.Interfaces;
using food_history_api.Services.Interfaces;

namespace food_history_api.Services;

public class RecipeService : IRecipeService{

    private readonly IRecipeDao _recipeDao;
    private readonly ICourseDao _courseDao;
    private readonly ICusineDao _cuisineDao;
    private readonly ITagDao _tagDao;

    public RecipeService(IRecipeDao recipeDao, ICourseDao courseDao, ICusineDao cusineDao, ITagDao tagDao) {
        _recipeDao = recipeDao;
        _courseDao = courseDao;
        _cuisineDao = cusineDao;
        _tagDao = tagDao;
    }

    public Recipe Get(int id)
    {
        Recipe recipe = _recipeDao.Get(id);
        recipe.CourseTypes = _courseDao.Get(id);
        recipe.CuisineTypes = _cuisineDao.Get(id);
        recipe.Tags = _tagDao.Get(id);
        return recipe;
    }

    public List<Recipe> GetAll()
    {
        List<Recipe> recipeList = _recipeDao.GetAll();
        recipeList.ForEach(recipe => {
            recipe.CourseTypes = _courseDao.Get((int)recipe.Id);
            recipe.CuisineTypes = _cuisineDao.Get((int)recipe.Id);
            recipe.Tags = _tagDao.Get((int)recipe.Id);
        });
        return recipeList;
    }

    public List<Recipe> GetForTags(List<string> tags)
    {
        List<Recipe> recipeList = _recipeDao.GetForTags(tags);
        recipeList.ForEach(recipe => {
            recipe.CourseTypes = _courseDao.Get((int)recipe.Id);
            recipe.CuisineTypes = _cuisineDao.Get((int)recipe.Id);
            recipe.Tags = _tagDao.Get((int)recipe.Id);
        });
        return recipeList;
    }

    public int Create(Recipe recipe)
    {
        int id = _recipeDao.Create(recipe);
        _courseDao.Create(recipe.CourseTypes, id);
        _cuisineDao.Create(recipe.CuisineTypes, id);
        _tagDao.Create(recipe.Tags, id);
        return id;
    }

    public void Update(Recipe recipe)
    {
        _recipeDao.Update(recipe);
        _courseDao.Delete((int)recipe.Id);
        _courseDao.Create(recipe.CourseTypes, (int)recipe.Id);
        _cuisineDao.Delete((int)recipe.Id);
        _cuisineDao.Create(recipe.CuisineTypes, (int)recipe.Id);
        _tagDao.Delete((int)recipe.Id);
        _tagDao.Create(recipe.Tags, (int)recipe.Id);
    }

    public void Delete(int id) => _recipeDao.Delete(id);

}