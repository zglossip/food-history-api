using food_history_api.Models;
using food_history_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace food_history_api.Controllers;

[ApiController]
[Route("recipe")]
public class RecipeController : ControllerBase {

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
        Recipe recipe = _recipeService.Get(id);

        if(recipe == null) {
            return NotFound();
        }

        FillOutRecipeLinks(recipe);

        return recipe;
    }

    [HttpGet]
    public ActionResult<List<Recipe>> GetAll() => _recipeService.GetAll();

    [HttpGet("tags")]
    public ActionResult<List<Recipe>> GetForTags(List<string> tags) => _recipeService.GetForTags(tags);

    private void FillOutRecipeLinks(Recipe recipe) {
       //TODO: Set recipe URLs
    }

    [HttpPost]
    public IActionResult Create(Recipe recipe) => CreatedAtAction(nameof(Get), new { id = _recipeService.Create(recipe) }, recipe);

    [HttpPut("{id}")]
    public IActionResult Update(int id, Recipe recipe) {
        if (id != recipe.Id) {
            return BadRequest();
        }

        if(_recipeService.Get(id) == null) {
            return NotFound();
        }

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

        //TODO: Set recipe URL

        return ingredientList;
        
    }

    [HttpPut("{id}/ingredients")]
    public IActionResult UpdateIngredients(int id, IngredientList ingredientList){
        if (id != ingredientList.RecipeId) {
            return BadRequest();
        }

        if(_recipeService.Get(id) == null) {
            return NotFound();
        }
        
        _ingredientService.Update(ingredientList);
        return NoContent();
    }

    [HttpGet("{id}/instructions")]
    public ActionResult<InstructionList> GetInstructions(int id) {
        InstructionList instructionList = _instructionService.Get(id);

        if(instructionList == null) {
            return NotFound();
        }

        //TODO: Set recipe URL

        return instructionList;
    }

    [HttpPut("{id}/instructions")]
    public IActionResult UpdateInstructions(int id, InstructionList instructionList){
        if (id != instructionList.RecipeId) {
            return BadRequest();
        }

        if(_recipeService.Get(id) == null) {
            return NotFound();
        }
        
        _instructionService.Update(instructionList);
        return NoContent();
    }

}