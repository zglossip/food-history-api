using food_history_api.Models;
using food_history_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace food_history_api.Controllers;

[ApiController]
[Route("controller")]
public class RecipeController : ControllerBase {

    [HttpGet("{id}")]
    public ActionResult<Recipe> Get(int id) => RecipeService.Get(id);

    [HttpGet]
    public ActionResult<List<Recipe>> GetAll() => RecipeService.GetAll();

    [HttpGet]
    public ActionResult<List<Recipe>> GetForTags(List<string> tags) => RecipeService.GetForTags(tags);

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

    [HttpPut("{id}/ingredients")]
    public IActionResult UpdateIngredients(int id, List<Ingredient> ingredients){
        if(RecipeService.Get(id) == null) {
            return NotFound();
        }
        
        IngredientService.Update(id, ingredients);
        return NoContent();
    }

    [HttpPut("{id}/instructions")]
    public IActionResult UpdateInstructions(int id, List<string> instructions){
        if(RecipeService.Get(id) == null) {
            return NotFound();
        }
        
        InstructionService.Update(id, instructions);
        return NoContent();
    }

}