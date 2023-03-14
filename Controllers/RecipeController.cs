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
    public ActionResult<List<Recipe>> GetAll()
    {
        List<Recipe> recipes =_recipeService.GetAll();
        recipes.ForEach(recipe => FillOutRecipeLinks(recipe));
        return recipes;
    }

    [HttpGet("courses")]
    public ActionResult<List<Recipe>> GetForCourses(List<string> cuisines)
    {
        List<Recipe> recipes = _recipeService.GetForCourses(cuisines);
        recipes.ForEach(recipe => FillOutRecipeLinks(recipe));
        return recipes;
    }

    [HttpGet("cusines")]
    public ActionResult<List<Recipe>> GetForCuisines(List<string> cuisines)
    {
        List<Recipe> recipes = _recipeService.GetForCuisines(cuisines);
        recipes.ForEach(recipe => FillOutRecipeLinks(recipe));
        return recipes;
    }

    [HttpGet("tags")]
    public ActionResult<List<Recipe>> GetForTags(List<string> tags)
    {
        
        List<Recipe> recipes = _recipeService.GetForTags(tags);
        recipes.ForEach(recipe => FillOutRecipeLinks(recipe));
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

        _fillOutIngredientListLinks(ingredientList);

        return ingredientList;
        
    }

    [HttpPost("{id}/ingredients")]
    public IActionResult UpsertIngredients(int id, IngredientList ingredientList){
        if (id != ingredientList.RecipeId) {
            return BadRequest();
        }

        if(_recipeService.Get(id) == null) {
            return NotFound();
        }
        
        _ingredientService.Upsert(ingredientList);
        return NoContent();
    }

    [HttpGet("{id}/instructions")]
    public ActionResult<InstructionList> GetInstructions(int id) {
        InstructionList instructionList = _instructionService.Get(id);

        if(instructionList == null) {
            return NotFound();
        }

        _fillOutInstructionListLinks(instructionList);

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

    private void FillOutRecipeLinks(Recipe recipe)
    {
        recipe.Link = UrlHelperExtensions.Action(Url, nameof(Get), new {id = recipe.Id});
        recipe.Ingredients = UrlHelperExtensions.Action(Url, nameof(GetIngredients), new {id = recipe.Id});
        recipe.Instructions = UrlHelperExtensions.Action(Url, nameof(GetInstructions), new {id = recipe.Id});
    }

    private void _fillOutIngredientListLinks(IngredientList ingredientList)
    {
        ingredientList.Recipe = UrlHelperExtensions.Action(Url, nameof(Get), new {id = ingredientList.RecipeId});
    }

    private void _fillOutInstructionListLinks(InstructionList instructionList)
    {
        instructionList.Recipe = UrlHelperExtensions.Action(Url, nameof(Get), new {id = instructionList.RecipeId});
    }

}