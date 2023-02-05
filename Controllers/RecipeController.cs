using food_history_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace food_history_api.Controllers;

[ApiController]
[Route("controller")]
public class RecipeController : ControllerBase {

    [HttpGet]
    public ActionResult<List<Recipe>> GetAll() {
        //TODO: Fill out
        return null;
    }

    [HttpGet]
    public ActionResult<List<Recipe>> GetForTags(List<string> tags) {
        //TODO: Fill out
        return null;
    }

    [HttpPost]
    public IActionResult Create(Recipe recipe) {
        //TODO: Fill out
        return null;
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Recipe recipe) {
        if (id != recipe.Id) {
            return BadRequest();
        }

        //TODO: Fill out
        return null;
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id) {
        //TODO Fill out
        return null;
    }

    [HttpPut("{id}/ingredients")]
    public IActionResult UpdateIngredients(int id, List<Ingredient> ingredients){
        //TODO: Fill out
        return null;
    }

    [HttpPut("{id}/instructions")]
    public IActionResult UpdateInstructions(int id, List<string> instructions){
        //TODO: Fill out
        return null;
    }

}