using food_history_api.Models;
using food_history_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace food_history_api.Controllers;

[ApiController]
[Route("recipe")]
public class RecipeController : ControllerBase {

    [HttpGet("{id}")]
    public ActionResult<Recipe> Get(int id) {
        Recipe recipe = RecipeService.Get(id);

        if(recipe == null) {
            return NotFound();
        }

        FillOutRecipeLinks(recipe);

        return recipe;
    }

    [HttpGet]
    public ActionResult<List<Recipe>> GetAll() => RecipeService.GetAll();

    [HttpGet("tags")]
    public ActionResult<List<Recipe>> GetForTags(List<string> tags) => RecipeService.GetForTags(tags);

    private void FillOutRecipeLinks(Recipe recipe) {
       //TODO: Set recipe URLs
    }

    [HttpPost]
    public IActionResult Create(Recipe recipe) => CreatedAtAction(nameof(Get), new { id = RecipeService.Create(recipe) }, recipe);

    [HttpPut("{id}")]
    public IActionResult Update(int id, Recipe recipe) {
        if (id != recipe.Id) {
            return BadRequest();
        }

        if(RecipeService.Get(id) == null) {
            return NotFound();
        }

        RecipeService.Update(recipe);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id) {
        if(RecipeService.Get(id) == null) {
            return NotFound();
        }

        RecipeService.Delete(id);
        return NoContent();
    }

    [HttpGet("{id}/ingredients")]
    public ActionResult<IngredientList> GetIngredients(int id) {
        
        IngredientList ingredientList = IngredientService.Get(id);

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

        if(RecipeService.Get(id) == null) {
            return NotFound();
        }
        
        IngredientService.Update(ingredientList);
        return NoContent();
    }

    [HttpGet("{id}/instructions")]
    public ActionResult<InstructionList> GetInstructions(int id) {
        InstructionList instructionList = InstructionService.Get(id);

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

        if(RecipeService.Get(id) == null) {
            return NotFound();
        }
        
        InstructionService.Update(instructionList);
        return NoContent();
    }

}