using food_history_api.Models;
using food_history_api.Models.Enums;
using food_history_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace food_history_api.Controllers;

[ApiController]
[Route("recipe")]
public class RecipeController : ControllerBase 
{

    private readonly IIngredientService _ingredientService;
    private readonly IInstructionService _instructionService;
    private readonly IRecipeService _recipeService;

    public RecipeController(IIngredientService ingredientService, IInstructionService instructionService, IRecipeService recipeService) {
        _ingredientService = ingredientService;
        _instructionService = instructionService;
        _recipeService = recipeService;
    }

    [HttpGet("{id}")]
    public ActionResult<Recipe> Get(int id) {
        Recipe? recipe = _recipeService.Get(id);

        if(recipe == null) {
            return NotFound();
        }

        return recipe;
    }

    [HttpGet]
    public ActionResult<List<Recipe>> Get([FromQuery(Name = "course")] List<string> courses, [FromQuery(Name = "cuisine")] List<string> cuisines, [FromQuery(Name = "tag")] List<string> tags, [FromQuery(Name = "sort")] string? sort, [FromQuery(Name = "reverse")] bool? reverse, [FromQuery(Name = "name")] string? name)
    {
        RecipeColumn? sortColumn = null;

        if(sort == "id")
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
    public IActionResult Create(Recipe recipe)
    {
        int id = _recipeService.Create(recipe);
        recipe.Id = id;
 
        return CreatedAtAction(nameof(Get), new { id = id }, recipe);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Recipe recipe) {
        if(_recipeService.Get(id) == null) {
            return NotFound();
        }

        recipe.Id = id;
        _recipeService.Update(recipe);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id) {
        if(_recipeService.Get(id) == null) {
            return NotFound();
        }

        _recipeService.Delete(id);
        return NoContent();
    }

    [HttpGet("{id}/ingredients")]
    public ActionResult<IngredientList> GetIngredients(int id) {
        
        IngredientList ingredientList = _ingredientService.Get(id);

        if(ingredientList == null) {
            return NotFound();
        }

        return ingredientList;
        
    }

    [HttpPut("{id}/ingredients")]
    public IActionResult UpdateIngredients(int id, IngredientList ingredientList){
        if(_recipeService.Get(id) == null) {
            return NotFound();
        }

        ingredientList.RecipeId = id;
        _ingredientService.Update(ingredientList);
        return NoContent();
    }

    [HttpGet("{id}/instructions")]
    public ActionResult<InstructionList> GetInstructions(int id) {
        InstructionList instructionList = _instructionService.Get(id);

        if(instructionList == null) {
            return NotFound();
        }

        return instructionList;
    }

    [HttpPut("{id}/instructions")]
    public IActionResult UpdateInstructions(int id, InstructionList instructionList){
        if(_recipeService.Get(id) == null) {
            return NotFound();
        }
        
        instructionList.RecipeId = id;
        _instructionService.Update(instructionList);
        return NoContent();
    }
}