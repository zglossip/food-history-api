using recipe_catalog_api.Models;
using recipe_catalog_api.Models.Enums;
using recipe_catalog_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace recipe_catalog_api.Controllers;

[ApiController]
[Route("recipe")]
public class RecipeController(IIngredientService ingredientService, IInstructionService instructionService, IRecipeService recipeService) : ControllerBase
{

    private readonly IIngredientService _ingredientService = ingredientService;
    private readonly IInstructionService _instructionService = instructionService;
    private readonly IRecipeService _recipeService = recipeService;

    [HttpGet("{id}")]
    public ActionResult<Recipe> Get(int id)
    {
        Recipe? recipe = _recipeService.Get(id);

        if (recipe == null)
        {
            return NotFound();
        }

        return recipe;
    }

    [HttpGet]
    public ActionResult<List<Recipe>> Get([FromQuery(Name = "course")] List<string> courses, [FromQuery(Name = "cuisine")] List<string> cuisines, [FromQuery(Name = "tag")] List<string> tags, [FromQuery(Name = "sort")] string? sort, [FromQuery(Name = "reverse")] bool? reverse, [FromQuery(Name = "name")] string? name)
    {
        RecipeColumn? sortColumn = null;

        if (sort == "id")
        {
            sortColumn = RecipeColumn.ID;
        }
        else if (sort == "name")
        {
            sortColumn = RecipeColumn.NAME;
        }

        List<Recipe> recipes = _recipeService.Get(courses, cuisines, tags, sortColumn, reverse, name);

        return recipes;
    }

    [HttpPost]
    public IActionResult CreateFull(FullRecipeRequest recipe)
    {
        int id = _recipeService.CreateFull(recipe);
        return StatusCode(201, new { id });
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, RecipeRequest recipe)
    {
        if (!_recipeService.Exists(id))
        {
            return NotFound();
        }

        _recipeService.Update(id, recipe);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!_recipeService.Exists(id))
        {
            return NotFound();
        }

        _recipeService.Delete(id);
        return NoContent();
    }

    [HttpGet("{id}/ingredients")]
    public ActionResult<IngredientList> GetIngredients(int id)
    {

        IngredientList ingredientList = _ingredientService.Get(id);

        if (ingredientList == null)
        {
            return NotFound();
        }

        return ingredientList;

    }

    [HttpPut("{id}/ingredients")]
    public IActionResult UpdateIngredients(int id, IngredientList ingredientList)
    {
        if (!_recipeService.Exists(id))
        {
            return NotFound();
        }

        ingredientList.RecipeId = id;
        _ingredientService.Update(ingredientList);
        return NoContent();
    }

    [HttpGet("{id}/instructions")]
    public ActionResult<InstructionList> GetInstructions(int id)
    {
        InstructionList instructionList = _instructionService.Get(id);

        if (instructionList == null)
        {
            return NotFound();
        }

        return instructionList;
    }

    [HttpPut("{id}/instructions")]
    public IActionResult UpdateInstructions(int id, InstructionList instructionList)
    {
        if (!_recipeService.Exists(id))
        {
            return NotFound();
        }

        instructionList.RecipeId = id;
        _instructionService.Update(instructionList);
        return NoContent();
    }
}