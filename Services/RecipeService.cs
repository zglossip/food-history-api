using food_history_api.Models;
using food_history_api.DAOs.Interfaces;
using food_history_api.Services.Interfaces;
using food_history_api.Models.Enums;

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

    public Recipe? Get(int id)
    {
        return _getPopulatedRecipe(_recipeDao.Get(id));
    }

    public List<Recipe> Get(List<string> courses, List<string> cuisines, List<string> tags, RecipeColumn? sortColumn, bool? reverse, string? name)
    {
        List<Recipe> recipes = _recipeDao.Get(courses, cuisines, tags, sortColumn, name).Select(_getPopulatedRecipe).ToList();

        switch(sortColumn)
        {
            case RecipeColumn.NAME:
                recipes.Sort((x,y) => (reverse != null && reverse.Value ? -1 : 1) * x.Name.CompareTo(y.Name));
                break;
            default:
                recipes.Sort((x,y) => (reverse != null && reverse.Value ? -1 : 1) * (x.Id < y.Id ? -1 : x.Id == y.Id ? 0 : 1));
                break;
        }

        return recipes;
    }

    private Recipe _getPopulatedRecipe(Recipe? recipe) 
    {
        if(recipe == null || recipe.Id == null)
        {
            throw new Exception("Cannot populate null recipe.");
        }
        Recipe newRecipe = recipe.Clone();
        newRecipe.CourseTypes = _courseDao.Get((int)recipe.Id);
        newRecipe.CuisineTypes = _cuisineDao.Get((int)recipe.Id);
        newRecipe.Tags = _tagDao.Get((int)recipe.Id);
        return newRecipe;
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
        if(recipe == null || recipe.Id == null)
        {
            throw new Exception("Cannot populate null recipe.");
        }
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