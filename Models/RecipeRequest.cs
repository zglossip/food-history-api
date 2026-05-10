using System.ComponentModel.DataAnnotations;

namespace recipe_catalog_api.Models;

public class RecipeRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public List<string> CourseTypes { get; set; } = [];

    [Required]
    public List<string> CuisineTypes { get; set; } = [];

    [Required]
    public List<string> Tags { get; set; } = [];

    public int ServingAmount { get; set; }

    [Required]
    public string ServingName { get; set; } = string.Empty;

    public string? Source { get; set; }
}
