using recipe_catalog_api.Models;
using recipe_catalog_api.DAOs.Interfaces;
using recipe_catalog_api.Services.Interfaces;
using recipe_catalog_api.Models.Enums;

namespace recipe_catalog_api.Services;

public class RecipeService(IRecipeDao recipeDao, ICourseDao courseDao, ICuisineDao cuisineDao, ITagDao tagDao) : IRecipeService
{
    private readonly IRecipeDao _recipeDao = recipeDao;
    private readonly ICourseDao _courseDao = courseDao;
    private readonly ICuisineDao _cuisineDao = cuisineDao;
    private readonly ITagDao _tagDao = tagDao;

    public Recipe? Get(int id)
    {
        return _getPopulatedRecipe(_recipeDao.Get(id));
    }

    public List<Recipe> Get(List<string> courses, List<string> cuisines, List<string> tags, RecipeColumn? sortColumn, bool? reverse, string? name)
    {
        List<Recipe> recipes = _recipeDao.Get(courses, cuisines, tags, sortColumn, name).Select(_getPopulatedRecipe).ToList();

        switch (sortColumn)
        {
            case RecipeColumn.NAME:
                recipes.Sort((x, y) => (reverse != null && reverse.Value ? -1 : 1) * x.Name.CompareTo(y.Name));
                break;
            default:
                recipes.Sort((x, y) => (reverse != null && reverse.Value ? -1 : 1) * (x.Id < y.Id ? -1 : x.Id == y.Id ? 0 : 1));
                break;
        }

        return recipes;
    }

    private Recipe _getPopulatedRecipe(Recipe? recipe)
    {
        if (recipe == null)
        {
            throw new Exception("Cannot populate null recipe.");
        }
        Recipe newRecipe = recipe.Clone();
        newRecipe.CourseTypes = _courseDao.Get(recipe.Id);
        newRecipe.CuisineTypes = _cuisineDao.Get(recipe.Id);
        newRecipe.Tags = _tagDao.Get(recipe.Id);
        return newRecipe;
    }

    public int Create(RecipeRequest recipe)
    {
        int id = _recipeDao.Create(recipe);
        _courseDao.Create(recipe.CourseTypes, id);
        _cuisineDao.Create(recipe.CuisineTypes, id);
        _tagDao.Create(recipe.Tags, id);
        return id;
    }

    public void Update(int id, RecipeRequest recipe)
    {
        _recipeDao.Update(id, recipe);
        _courseDao.Delete(id);
        _courseDao.Create(recipe.CourseTypes, id);
        _cuisineDao.Delete(id);
        _cuisineDao.Create(recipe.CuisineTypes, id);
        _tagDao.Delete(id);
        _tagDao.Create(recipe.Tags, id);
    }

    public void Delete(int id) => _recipeDao.Delete(id);
}